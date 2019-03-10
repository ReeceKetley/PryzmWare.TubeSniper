using System;
using System.Collections.Generic;
using System.Threading;
using TubeSniper.Application.Youtube;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Common;
using TubeSniper.Domain.Interfaces;

namespace TubeSniper.Application.Campaigns
{
	public class ThreadedCampaign
	{
		private readonly Campaign _campaign;
		private readonly ICommentService _commentService;
		private readonly ISearchService _searchService;

		public ThreadedCampaign(Campaign campaign)
		{
			_campaign = campaign;
			_commentService = GlobalContainer.Container.Resolve<ICommentService>();
			_searchService = GlobalContainer.Container.Resolve<ISearchService>();
		}


		public void Start()
		{
			_searchService.Init();
			var videos = _searchService.SearchVideos(_campaign.Keyword, _campaign.MaxComments);
			_campaign.Start(videos);
			var threads = new List<Thread>();
			Console.WriteLine(_campaign.CommentPosters.Count);
			foreach (var commentPoster in _campaign.CommentPosters)
			{
				var thread = new Thread(ThreadMain);
				threads.Add(thread);
				thread.Start(commentPoster);
			}
		}

		public void Stop()
		{
			_campaign.Stop();
		}

		private void ThreadMain(object poster)
		{
			var commentPoster = (CommentPoster) poster;
			for (;;)
			{
				Console.WriteLine("Main Loop");
				var next = commentPoster.Next();
				if (next.Job == null)
				{
					Console.WriteLine("1");

					return;
				}

				var postCommentResult = _commentService.PostComment(next.Job);
				// adjust campaign
				commentPoster.Process(new CommentPostedEvent());
				if (postCommentResult == null)
				{
					// TODO: make an enum for result.
					// comment failed
					//_campaign.Process(new CommentFailed());
					continue;
				}

				//_campaign.Process(new CommentPosted());
			}
		}
	}
}