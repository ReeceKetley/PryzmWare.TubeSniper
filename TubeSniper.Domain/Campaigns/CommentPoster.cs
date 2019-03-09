using System;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class CommentPoster : ICommentPoster
	{
		private YoutubeAccount _account;
		private HttpProxy _proxy;

		public void Run(YoutubeAccount account, HttpProxy proxy, YoutubeVideo video, CommentGenerator comment, bool asReply, ICaptchaService captchaService)
		{
			_account = account;
			_proxy = proxy;
			var bot = new YoutubeBot(account, proxy, video, comment.Generate(), true, asReply, captchaService);
			bot.StatusChanged += Bot_StatusChanged;
			bot.Error += Bot_Error;
			bot.VideoProcessed += Bot_VideoProcessed;
			bot.Run();
		}

		public event EventHandler<FatalErrorEventArgs> FatalError;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		private void Bot_VideoProcessed(object sender, VideoProcessedEventArgs e)
		{
			var comment = new SuccessComment();
			comment.Proxy = "N/A";
			if (_proxy != null)
			{
				comment.Proxy = _proxy.Address.Host + ":" + _proxy.Address.Port;
			}

			comment.Comment = e.Comment;
			comment.Email = _account.Credentials.Email;
			OnVideoProcessed(e);
		}

		private void Bot_Error(object sender, FatalErrorEventArgs e)
		{
			Console.WriteLine("Job: [Error Event] : {0}", e.Error);
			OnFatalError(e);
		}

		private void Bot_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			Console.WriteLine("Job: [Status Event] : {0}", e.Status);
			OnStatusChanged(e);
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
	}
}