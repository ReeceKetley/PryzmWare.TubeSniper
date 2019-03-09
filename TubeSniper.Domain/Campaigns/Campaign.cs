using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TubeSniper.Domain.Interfaces;
using TubeSniper.Domain.Models;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class Campaign
	{
		private readonly IAccountRegister _accountRegister;
		private readonly IYoutubeCommentBotFactory _botFactory;
		private readonly ICaptchaService _captchaService;
		private readonly CommentGenerator _commentGenerator;
		private readonly IProxyTestService _proxyTestService;
		private readonly ISearchService _searchService;
		private ThreadSafeVideoStack _videoStack;

		private IEnumerable<CampaignWorker> _workers;

		public Campaign(Campaign source)
		{
			Id = source.Id;
			if (source.Meta != null)
			{
				Meta = new CampaignMeta(source.Meta);
			}
		}

		public Campaign(ProxyCollection proxyCollection, CampaignMeta meta, IAccountRegister accountRegister, string searchTerm, CommentGenerator commentGenerator, bool asReply, ISearchService searchService, ICaptchaService captchaService, IProxyTestService proxyTestService, IYoutubeCommentBotFactory botFactory)
		{
			ProxyCollection = proxyCollection;
			meta.SearchTerm = searchTerm;
			Meta = meta;
			this._accountRegister = accountRegister;
			_commentGenerator = commentGenerator;
			_searchService = searchService;
			_captchaService = captchaService;
			_proxyTestService = proxyTestService;
			_botFactory = botFactory;
		}

		public bool AsReply { get; set; }
		public CampaignMeta Meta { get; }
		public bool Editable { get; private set; }
		public ProxyCollection ProxyCollection { get; set; }
		public string Comment { get; set; }
		public Guid Id { get; set; }
		public int MaxResults { get; set; }
		public ObservableCollection<string> StatusLog { get; } = new ObservableCollection<string>();
		public YoutubeVideo VideoMeta { get; set; }
		public int Workers { get; set; }

		public event EventHandler CampaignStarted;
		public event EventHandler CampaignStopped;

		public event EventHandler<FatalErrorEventArgs> FatalError;

		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		public void Start()
		{
			_searchService.Init();
			var videos = _searchService.SearchVideos(Meta.SearchTerm, MaxResults);
			var posters = CreatePosters(videos);
			_videoStack = new ThreadSafeVideoStack(videos.Randomize());
			_workers = CreateWorkers(Workers);
			foreach (var campaignWorker in _workers)
			{
				campaignWorker.Start();
			}

			OnCampaignStarted();
			Editable = false;
		}

		public void Stop()
		{
			foreach (var campaignWorker in _workers)
			{
				campaignWorker.Stop();
			}

			OnCampaignStopped();
			Editable = true;
		}

		private IEnumerable<CommentPoster> CreatePosters(List<YoutubeAccount> accounts, List<YoutubeVideo> videos)
		{
			foreach (var youtubeVideo in videos)
			{
				new CommentPoster(null, )
			}
		}

		private IEnumerable<CampaignWorker> CreateWorkers(int workerCount)
		{
			var workers = new List<CampaignWorker>();
			for (int i = 0; i < workerCount; i++)
			{
				var worker = new CampaignWorker();
				workers.Add(worker);
			}

			return workers;
		}

		private void CampaignWorker_FatalError(object sender, FatalErrorEventArgs e)
		{
			OnFatalError(e);
		}

		private void CampaignWorker_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			OnStatusChanged(e);
		}

		private void CampaignWorker_VideoProcessed(object sender, VideoProcessedEventArgs e)
		{
			var id = e.Video.Id;
			Meta.VideoProccesed.Add(id);
			VideoMeta = e.Video;
			OnVideoProcessed(e);
			SharedData.OnCampaignProcessed(new CampaignProcessedEventArgs(e.Video, e.Comment, Meta));
		}

		protected virtual void OnCampaignStarted()
		{
			CampaignStarted?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnCampaignStopped()
		{
			CampaignStopped?.Invoke(this, EventArgs.Empty);
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

		private CommentPoster CreateJob()
		{
			var job = new CommentPoster();
			job.FatalError += (sender, args) => OnFatalError(args);
			job.StatusChanged += (sender, args) => OnStatusChanged(args);
			job.VideoProcessed += (sender, args) => OnVideoProcessed(args);
			return job;
		}
	}

	public class CommentJob
	{
		public YoutubeAccount Account { get; }
		public HttpProxy Proxy { get; }
		public YoutubeVideo Video { get; }
		public Comment Comment { get; }
		public CommentMethod CommentMethod { get; }

		public CommentJob(YoutubeAccount account, HttpProxy proxy, YoutubeVideo video, Comment comment, CommentMethod commentMethod)
		{
			Account = account;
			Proxy = proxy;
			Video = video;
			Comment = comment;
			CommentMethod = commentMethod;
		}
	}
}