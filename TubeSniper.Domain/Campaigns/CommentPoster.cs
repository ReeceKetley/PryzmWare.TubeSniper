using System;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class CommentPoster
	{
		private readonly ICaptchaService _captchaService;
		private readonly IYoutubeCommentBotFactory _botFactory;

		public CommentPoster(ICaptchaService captchaService, IYoutubeCommentBotFactory botFactory)
		{
			_captchaService = captchaService;
			_botFactory = botFactory;
		}


		public void Run(CommentJob commentJob)
		{
			var bot = new YoutubeBot(commentJob, _captchaService, _botFactory);
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