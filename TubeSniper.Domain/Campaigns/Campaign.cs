using System;
using System.Collections.Generic;
using TubeSniper.Domain.Accounts;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	/*public Campaign(Guid id, CampaignTitle title, Keyword keyword, IEnumerable<AccountEntry> accounts, IEnumerable<ProxyEntry> proxies, CommentMethod commentMethod, int maxComments, int numberOfWorkers, IEnumerable<VideoId> processedVideos)
		{
			// todo: VALIDATE (null checks)

			Id = id;
			Title = title;
			Keyword = keyword;
			Accounts = accounts != null ? new List<AccountEntry>(accounts) : new List<AccountEntry>();
			Proxies = proxies != null ? new List<ProxyEntry>(proxies) : new List<ProxyEntry>();
			CommentMethod = commentMethod;
			MaxComments = maxComments;
			NumberOfWorkers = numberOfWorkers;
			ProcessedVideos = processedVideos != null ? new List<VideoId>(processedVideos) : new List<VideoId>();

			
		}*/
	public class Campaign
	{
		private StandardAccountRegister _accountRegister;
		private VideoCollection _videos;

		public Campaign()
		{
		}

		// Config
		public Guid Id { get; set; }
		public CampaignTitle Title { get; set; }
		public Keyword Keyword { get; set; }
		public List<AccountEntry> Accounts { get; set; } = new List<AccountEntry>();	
		public List<ProxyEntry> Proxies { get; set; } = new List<ProxyEntry>();
		public Comment Comment { get; set; }
		public CommentMethod CommentMethod { get; set; }
		public int MaxComments { get; set; }
		public int NumberOfWorkers { get; set; }
		public List<VideoId> ProcessedVideos { get; set; } = new List<VideoId>();


		public bool IsRunning { get; private set; }
		public int ErrorCount { get; private set; }
		public int SuccessCount { get; private set; }
		public List<CommentPoster> CommentPosters { get; private set; }

		private List<CommentPoster> CreatePosters()
		{
			var posters = new List<CommentPoster>();
			for (int i = 0; i < NumberOfWorkers; i++)
			{
				posters.Add(new CommentPoster(this));
			}

			return posters;
		}

		public event EventHandler Started;
		public event EventHandler Stopped;
		public event EventHandler ErrorCountChanged;
		public event EventHandler SuccessCountChanged;

		public void Start(List<YoutubeVideo> videos)
		{
			CommentPosters = CreatePosters();
			_videos = new VideoCollection(new List<YoutubeVideo>(videos));
			_accountRegister = new StandardAccountRegister(Accounts);
			IsRunning = true;
			OnStarted();
		}

		public void Stop()
		{
			ResetMetrics();
			IsRunning = false;
			OnStopped();
		}

		private void ResetMetrics()
		{
			ErrorCount = 0;
			OnErrorCountChanged();
			SuccessCount = 0;
			OnSuccessCountChanged();
		}

		public void IncreaseErrorCount()
		{
			++ErrorCount;
			OnErrorCountChanged();
		}

		public void IncreaseSuccessCount()
		{
			++SuccessCount;
			OnSuccessCountChanged();
		}

		public CommentJob NextJob()
		{
			var account = _accountRegister.Acquire();
			if (account == null)
			{
				return null;
			}

			ProxyEntry proxy = null;
			if (Proxies.Count > 0)
			{
				// TODO: randomise
				proxy = Proxies[0];
			}

			var video = _videos.Next();
			if (video == null)
			{
				_accountRegister.Release(account);
				return null;
			}

			var commentGenerator = new CommentGenerator(new CommentTemplate(Comment.Value));
			return new CommentJob(account, proxy?.Proxy, video, commentGenerator.Generate(), CommentMethod);
		}

		protected virtual void OnErrorCountChanged()
		{
			ErrorCountChanged?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnSuccessCountChanged()
		{
			SuccessCountChanged?.Invoke(this, EventArgs.Empty);
		}

		public void Process(CommentPostedEvent commentPostedEvent)
		{
			IncreaseSuccessCount();
		}

		protected virtual void OnStarted()
		{
			Started?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnStopped()
		{
			Stopped?.Invoke(this, EventArgs.Empty);
		}

		public bool CheckIsValid()
		{
			// todo: ADD business logic
			return true;
		}
	}
}