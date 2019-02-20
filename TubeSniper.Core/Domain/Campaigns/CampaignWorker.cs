using System;
using System.Net;
using System.Threading;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Models;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class CampaignWorker
	{
		public CommentRegister CommentTemplate { get; }
		private readonly IAccountRegister _accountRegister;
		private readonly ThreadSafeVideoStack _videoStack;
		private readonly bool _asReply;
		private readonly ProxyRegister _proxyRegister;
		private bool _shouldStop;
		public event EventHandler<FatalErrorEventArgs> FatalError;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;
		private Thread _thread;

		public CampaignWorker(IAccountRegister accountRegister, ThreadSafeVideoStack videoStack, CommentRegister commentTemplate, bool asReply, ProxyRegister proxyRegister = null)
		{
			CommentTemplate = commentTemplate;
			_accountRegister = accountRegister;
			_videoStack = videoStack;
			_asReply = asReply;
			_proxyRegister = proxyRegister;
			_thread = new Thread(ThreadMain);
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

		private void ThreadMain()
		{
			YoutubeAccount ytAccount = null;
			while (!_shouldStop)
			{
				Console.WriteLine("Thread Main");
				var job = new Job();
				job.FatalError += (sender, args) => OnFatalError(args);
				job.StatusChanged += (sender, args) => OnStatusChanged(args);
				job.VideoProcessed += (sender, args) => OnVideoProcessed(args);
				var nextVideo = _videoStack.Next();
				if (nextVideo == null)
				{
					break;
				}


				WebProxy proxy = null;

				if (_proxyRegister.HasProxies)
				{
					for (; ; )
					{
						var proxyEntry = _proxyRegister.Aquire();
						proxy = proxyEntry.Proxy;
						SharedData.OnCurrentStep(CurrentStepEventArgs.EstablishingProxyConnection);
						var proxyTestResult = ProxyTestService.TestProxy(proxyEntry);
						if (!proxyTestResult.Active)
						{
							OnStatusChanged(new StatusChangedEventArgs("Proxy connection failed (" + proxy.Address.Host + "). Trying new proxy."));
							SharedData.OnCurrentStep(CurrentStepEventArgs.EstablishingProxyConnectionFailled);
							continue;
						}

						OnStatusChanged(new StatusChangedEventArgs("Proxy connection established. (" + proxyEntry.Proxy.Address.Host + ":" + proxyEntry.Proxy.Address.Port + ") - Response Time: " + proxyTestResult.ResponseTime + "ms"));
						SharedData.OnCurrentStep(CurrentStepEventArgs.ProxyConectionEstablished);
						break;
					}
				}

				var accounts = _accountRegister.GetAll();
				Console.WriteLine("Campaign Worker");
				ytAccount = _accountRegister.Acquire();
				while (ytAccount == null)
				{
					Thread.Sleep(1000);
					ytAccount = _accountRegister.Acquire();
				}

				Console.WriteLine("Aquired account");
				job.Run(ytAccount, proxy, nextVideo, CommentTemplate, _asReply);
			}

			_accountRegister.Release(ytAccount);
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

		public void Stop()
		{
			if (!_shouldStop)
			{
				_shouldStop = true;
			}
			_thread.Abort();
			OnStatusChanged(new StatusChangedEventArgs("Stopping worker."));
		}
	}
}