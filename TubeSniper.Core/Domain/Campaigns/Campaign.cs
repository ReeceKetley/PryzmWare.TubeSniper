using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Com.CloudRail.SI.Types;
using TubeSniper.Core.Domain.Models;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class CampaignMeta
	{
		public CampaignMeta()
		{
		}

		public CampaignMeta(CampaignMeta source)
		{
			Title = source.Title;
			if (source.Accounts != null)
			{
				Accounts = new List<YoutubeAccount>();
				foreach (var account in source.Accounts)
				{
					Accounts.Add(new YoutubeAccount(account));
				}
			}

			if (source.VideoProccesed != null)
			{
				VideoProccesed = new List<string>(source.VideoProccesed);
			}

			SearchTerm = source.SearchTerm;
		}

		public string Title { get; set; }
		public List<YoutubeAccount> Accounts { get; set; } = new List<YoutubeAccount>();
		public ProxyRegister ProxyRegister { get; set; }
		public List<string> VideoProccesed { get; set; } = new List<string>();
		public string SearchTerm { get; set; }
	}

	public class Campaign
	{
		private readonly IAccountRegister _accountRegister;
		private readonly CommentRegister _commentRegister;
		private CampaignVideoResigter _videoResigter;

		private IEnumerable<CampaignWorker> _workers;

		public Campaign(CampaignMeta campaignMeta, IAccountRegister accountRegister, string searchTerm, string baseComment, Dictionary<string, string> variables, bool asReply)
		{
			Variables = variables;
			campaignMeta.SearchTerm = searchTerm;
			CampaignMeta = campaignMeta;
			this._accountRegister = accountRegister;
			BaseComment = baseComment;
			_commentRegister = new CommentRegister(baseComment, Variables);
		}

		public Campaign(Campaign source)
		{
			Id = source.Id;
			BaseComment = source.BaseComment;
			if (source.CampaignMeta != null)
			{
				CampaignMeta = new CampaignMeta(source.CampaignMeta);
			}
		}

		public Guid Id { get; set; }
		public string BaseComment { get; }
		public CampaignMeta CampaignMeta { get; }
		public Dictionary<string, string> Variables { get; }
		public string Thumbnail { get; private set; }
		public string Status { get; private set; }
		public bool Editable { get; private set; }
		public int Workers { get; set; } = 2;
		public int MaxResults { get; set; } = 20;
		public VideoMetaData VideoMeta { get; set; }
		public ObservableCollection<string> StatusLog { get; private set; } = new ObservableCollection<string>();

		public bool AsReply { get; set; }

		public event EventHandler<FatalErrorEventArgs> FatalError;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;
		public event EventHandler NetworkError;
		public event EventHandler CampaignStarted;
		public event EventHandler CampaignStopped;

		public void Start()
		{
			SharedData.OnCurrentStep(CurrentStepEventArgs.Searching);
			var videos = SearchService.SearchVideos(CampaignMeta.SearchTerm, MaxResults);
			_videoResigter = new CampaignVideoResigter(videos);
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

		private void CampaignWorker_VideoProcessed(object sender, VideoProcessedEventArgs e)
		{
			var id = e.Meta.GetId();
			CampaignMeta.VideoProccesed.Add(id);
			VideoMeta = e.Meta;
			OnVideoProcessed(e);
			SharedData.OnCurrentStep(CurrentStepEventArgs.CommentPosted);
			SharedData.OnCampaignProcessed(new CampaignProcessedEventArgs(e.Meta, e.Comment, CampaignMeta));
		}

		private void CampaignWorker_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			OnStatusChanged(e);
		}

		private void CampaignWorker_FatalError(object sender, FatalErrorEventArgs e)
		{
			OnFatalError(e);
		}

		private IEnumerable<CampaignWorker> CreateWorkers(int workerCount)
		{
			var workers = new List<CampaignWorker>();
			for (int i = 0; i < workerCount; i++)
			{
				var worker = new CampaignWorker(_accountRegister, _videoResigter, _commentRegister, AsReply, CampaignMeta.ProxyRegister);
				workers.Add(worker);
			}

			return workers;
		}

		protected virtual void OnFatalError(FatalErrorEventArgs e)
		{
			FatalError?.Invoke(this, e);
		}

		protected virtual void OnStatusChanged(StatusChangedEventArgs e)
		{
			StatusChanged?.Invoke(this, e);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnVideoProcessed(VideoProcessedEventArgs e)
		{
			VideoProcessed?.Invoke(this, e);
		}

		protected virtual void OnCampaignStarted()
		{
			CampaignStarted?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnCampaignStopped()
		{
			CampaignStopped?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnNetworkError()
		{
			NetworkError?.Invoke(this, EventArgs.Empty);
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