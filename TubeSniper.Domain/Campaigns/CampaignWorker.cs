using System;
using System.Threading;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class CampaignWorker
	{
		private readonly CommentPoster _commentPoster;
		private bool _shouldStop;
		private readonly Thread _thread;

		public CampaignWorker(CommentPoster commentPoster)
		{
			_commentPoster = commentPoster;
			_thread = new Thread(ThreadMain);

		}

		private void ThreadMain()
		{
			_commentPoster.Run();
		}

		public void Start()
		{
			if (_shouldStop)
			{
				_shouldStop = false;
			}
			var thread = new Thread(ThreadMain);
			thread.Start();
		}

		public void Stop()
		{
			if (!_shouldStop)
			{
				_shouldStop = true;
			}
			_thread.Abort();
		}

	}
}