using System;
using System.Net;
using System.Threading;
using TubeSniper.Core.Domain.Models;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class CampaignWorker
	{
		private readonly IAccountRegister _accountRegister;
		private readonly bool _asReply;
		private readonly ProxyRegister _proxyRegister;
		private bool _shouldStop;
		private Thread _thread;
		private readonly ThreadSafeVideoStack _videoStack;

		public CampaignWorker(IAccountRegister accountRegister, ThreadSafeVideoStack videoStack, CommentRegister commentTemplate, bool asReply, ProxyRegister proxyRegister = null)
		{
			CommentTemplate = commentTemplate;
			_accountRegister = accountRegister;
			_videoStack = videoStack;
			_asReply = asReply;
			_proxyRegister = proxyRegister;
			_thread = new Thread(ThreadMain);
		}

		public event EventHandler<FatalErrorEventArgs> FatalError;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		private void ThreadMain()
		{
			YoutubeAccount account = null;
			while (!_shouldStop)
			{
				var job = new Job();
				job.FatalError += (sender, args) => OnFatalError(args);
				job.StatusChanged += (sender, args) => OnStatusChanged(args);
				job.VideoProcessed += (sender, args) => OnVideoProcessed(args);
				var nextVideo = _videoStack.Next();
				if (nextVideo == null)
				{
					break;
				}

				HttpProxy proxy = null;
				if (_proxyRegister.HasProxies)
				{
					for (; ; )
					{
						var proxyEntry = _proxyRegister.Aquire();
						proxy = proxyEntry.Proxy;
						SharedData.OnCurrentStep(CurrentStepEventArgs.EstablishingProxyConnection);
						var proxyTestResult = ProxyTestService.TestProxy(proxyEntry.Proxy);
						if (!proxyTestResult.Active)
						{
							OnStatusChanged(new StatusChangedEventArgs("Proxy connection failed (" + proxy.Address.Host + "). Trying new proxy."));
							SharedData.OnCurrentStep(CurrentStepEventArgs.EstablishingProxyConnectionFailled);
							_proxyRegister.Remove(proxyEntry);
							continue;
						}

						OnStatusChanged(new StatusChangedEventArgs("Proxy connection established. (" + proxyEntry.Proxy.Address.Host + ":" + proxyEntry.Proxy.Address.Port + ") - Response Time: " + proxyTestResult.ResponseTime + "ms"));
						SharedData.OnCurrentStep(CurrentStepEventArgs.ProxyConectionEstablished);
						break;
					}
				}

				for (;;)
				{
					account = _accountRegister.Acquire();
					if (account != null)
					{
						break;
					}
					Thread.Sleep(1000);
				}

				job.Run(account, proxy, nextVideo, CommentTemplate, _asReply);
				_accountRegister.Release(account);
			}

			OnStatusChanged(new StatusChangedEventArgs("Worker finished."));
		}

		protected virtual void OnFatalError(FatalErrorEventArgs e)
		{ 
			FatalError?.Invoke(this, e);
		}

		protected virtual void OnStatusChanged(StatusChangedEventArgs e)
		{
			StatusChanged?.Invoke(this, e);
		}

		protected virtual void OnVideoProcessed(VideoProcessedEventArgs e)
		{
			VideoProcessed?.Invoke(this, e);
		}

		public void Start()
		{
			if (_shouldStop)
			{
				_shouldStop = false;
			}
			var thread = new Thread(ThreadMain);
			thread.Start();
			OnStatusChanged(new StatusChangedEventArgs("Started worker."));
		}

		public void Stop()
		{
			if (!_shouldStop)
			{
				_shouldStop = true;
			}
			_thread.Abort();
			OnStatusChanged(new StatusChangedEventArgs("Stopping worker."));
		}

		public CommentRegister CommentTemplate { get; }
	}
}