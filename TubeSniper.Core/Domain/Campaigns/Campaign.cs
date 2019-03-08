using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TubeSniper.Core.Domain.Models;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class Campaign
	{
		private readonly IAccountRegister _accountRegister;
		private readonly CommentRegister _commentRegister;
		private readonly ISearchService _searchService;
		private ThreadSafeVideoStack _videoStack;

		private IEnumerable<CampaignWorker> _workers;

		public Campaign(Campaign source)
		{
			Id = source.Id;
			BaseComment = source.BaseComment;
			if (source.CampaignMeta != null)
			{
				CampaignMeta = new CampaignMeta(source.CampaignMeta);
			}
		}

		public Campaign(CampaignMeta campaignMeta, IAccountRegister accountRegister, string searchTerm, string baseComment, Dictionary<string, string> variables, bool asReply, ISearchService searchService)
		{
			Variables = variables;
			campaignMeta.SearchTerm = searchTerm;
			CampaignMeta = campaignMeta;
			this._accountRegister = accountRegister;
			_searchService = searchService;
			BaseComment = baseComment;
			_commentRegister = new CommentRegister(baseComment, Variables);
		}

		public bool AsReply { get; set; }
		public string BaseComment { get; }
		public CampaignMeta CampaignMeta { get; }
		public bool Editable { get; private set; }

		public Guid Id { get; set; }
		public int MaxResults { get; set; }
		public ObservableCollection<string> StatusLog { get; } = new ObservableCollection<string>();
		public Dictionary<string, string> Variables { get; }
		public YoutubeVideo VideoMeta { get; set; }
		public int Workers { get; set; }

		public event EventHandler CampaignStarted;
		public event EventHandler CampaignStopped;

		public event EventHandler<FatalErrorEventArgs> FatalError;
		public event EventHandler NetworkError;

		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

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
			CampaignMeta.VideoProccesed.Add(id);
			VideoMeta = e.Video;
			OnVideoProcessed(e);
			SharedData.OnCurrentStep(CurrentStepEventArgs.CommentPosted);
			SharedData.OnCampaignProcessed(new CampaignProcessedEventArgs(e.Video, e.Comment, CampaignMeta));
		}

		private IEnumerable<CampaignWorker> CreateWorkers(int workerCount)
		{
			var workers = new List<CampaignWorker>();
			for (int i = 0; i < workerCount; i++)
			{
				var worker = new CampaignWorker(_accountRegister, _videoStack, _commentRegister, AsReply, CampaignMeta.ProxyRegister);
				workers.Add(worker);
			}

			return workers;
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

		protected virtual void OnNetworkError()
		{
			NetworkError?.Invoke(this, EventArgs.Empty);
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
			_searchService.Init();
			//SharedData.OnCurrentStep(CurrentStepEventArgs.Searching);
			var videos = _searchService.SearchVideos(CampaignMeta.SearchTerm, MaxResults);
			_videoStack = new ThreadSafeVideoStack(videos.Randomize());
			_workers = CreateWorkers(Workers);
			foreach (var campaignWorker in _workers)
			{
				campaignWorker.FatalError += CampaignWorker_FatalError;
				campaignWorker.StatusChanged += CampaignWorker_StatusChanged;
				campaignWorker.VideoProcessed += CampaignWorker_VideoProcessed;
				campaignWorker.Start();
			}

			OnCampaignStarted();
			Editable = false;
			//Console.WriteLine("Campaign ID: " + Id);
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

		
	}

	public static class IEnumerableExtensions
	{

		public static List<YoutubeVideo> Randomize(this List<YoutubeVideo> target)
		{
			Random r = new Random();

			return new List<YoutubeVideo>(target.OrderBy(x => (r.Next())));
		}
	}

	public enum CurrentStepEventArgs
	{
		Searching,
		Downloading,
		Commenting,
		LogingIn,
		EstablishingProxyConnection,
		Failure,
		CommentPosted,
		EstablishingProxyConnectionFailled,
		ProxyConectionEstablished,
		LoggedIn
	}
}