using System;
using System.Threading;
using TubeSniper.Domain.Models;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class CampaignWorker
	{
		private readonly IAccountRegister _accountRegister;
		private readonly bool _asReply;
		private readonly ICaptchaService _captchaService;
		private readonly IProxyTestService _proxyTestService;
		private readonly ProxyCollection _proxyCollection;
		private bool _shouldStop;
		private readonly Thread _thread;
		private readonly ThreadSafeVideoStack _videoStack;
		private readonly CommentGenerator _commentTemplate;

		public CampaignWorker(IAccountRegister accountRegister, ThreadSafeVideoStack videoStack, CommentGenerator commentTemplate, bool asReply, ICaptchaService captchaService, IProxyTestService proxyTestService, ProxyCollection proxyCollection = null)
		{
			_accountRegister = accountRegister;
			_videoStack = videoStack;
			_commentTemplate = commentTemplate;
			_asReply = asReply;
			_captchaService = captchaService;
			_proxyTestService = proxyTestService;
			_proxyCollection = proxyCollection;
			_thread = new Thread(ThreadMain);
		}

		public event EventHandler<FatalErrorEventArgs> FatalError;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		private void ThreadMain()
		{
			while (!_shouldStop)
			{
				CommentPoster job = CreateJob();
				var nextVideo = _videoStack.Next();
				if (nextVideo == null)
				{
					break;
				}

				var proxy = GetProxy();
				YoutubeAccount account = GetAccount();

				job.Run(account, proxy, nextVideo, _commentTemplate, _asReply, _captchaService);
				_accountRegister.Release(account);
			}

			OnStatusChanged(new StatusChangedEventArgs("Worker finished."));
		}

		private YoutubeAccount GetAccount()
		{
			YoutubeAccount account = null;
			for (; ; )
			{
				account = _accountRegister.Acquire();
				if (account != null)
				{
					break;
				}
				Thread.Sleep(1000);
			}

			return account;
		}

		private HttpProxy GetProxy()
		{
			if (_proxyCollection.HasProxies)
			{
				for (; ; )
				{
					var proxyEntry = _proxyCollection.Aquire();
					var proxyTestResult = _proxyTestService.TestProxy(proxyEntry.Proxy);
					if (!proxyTestResult.Active)
					{
						OnStatusChanged(new StatusChangedEventArgs("Proxy connection failed (" + proxyEntry.Proxy.Address.Host + "). Trying new proxy."));
						_proxyCollection.Remove(proxyEntry);
						continue;
					}

					OnStatusChanged(new StatusChangedEventArgs("Proxy connection established. (" + proxyEntry.Proxy.Address.Host + ":" + proxyEntry.Proxy.Address.Port + ") - Response Time: " + proxyTestResult.ResponseTime + "ms"));
					return proxyEntry.Proxy;
				}
			}

			return null;
		}

		private CommentPoster CreateJob()
		{
			var job = new CommentPoster();
			job.FatalError += (sender, args) => OnFatalError(args);
			job.StatusChanged += (sender, args) => OnStatusChanged(args);
			job.VideoProcessed += (sender, args) => OnVideoProcessed(args);
			return job;
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

	}
}