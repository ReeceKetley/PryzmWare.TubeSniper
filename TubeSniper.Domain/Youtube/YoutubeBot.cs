using System;
using System.IO;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Youtube
{
	public class YoutubeBot
	{
		private readonly CommentJob _commentJob;
		private readonly ICaptchaService _captchaService;
		private readonly IYoutubeCommentBotFactory _botFactory;
		public readonly VirtualBrowser.VirtualBrowser Browser;
		public event EventHandler<FatalErrorEventArgs> Error;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		public YoutubeBot(CommentJob commentJob, ICaptchaService captchaService, IYoutubeCommentBotFactory botFactory)
		{
			_commentJob = commentJob;
			_captchaService = captchaService;
			_botFactory = botFactory;
			var cacheId = GetCacheId();
			Browser = VirtualBrowser.VirtualBrowser.Create(commentJob.Proxy.ToWebProxy(), cacheId);
		}

		private CacheId GetCacheId()
		{
			return new CacheId(GeneralHelpers.MakeValidFileName(_commentJob.Account.Credentials.Email) + "-" + (_commentJob.Proxy != null ? GeneralHelpers.MakeValidFileName(_commentJob.Proxy.Address.ToString()) : null));
		}

		public HttpProxy Proxy { get; set; }

		public void Run()
		{
			UpdateStatus("Logging in: " + _commentJob.Account.Credentials.Email);
			var loginResult = Login().Code;
			if (loginResult != LoginResultCode.Success)
			{
				ThrowErrorEvent("Failed to login to account: + " + _commentJob.Account.Credentials.Email + ". (" + loginResult + ")");
			}

			UpdateStatus("Logged in: " + _commentJob.Account.Credentials.Email);
			UpdateStatus("Posting Comment: " + _commentJob.Video.Title);

			var commentPostedResult = Browser_postComment();
			if (commentPostedResult.Code != CommentPostedResultCode.Success)
			{
				ThrowErrorEvent("Comment post failure");
				UpdateStatus("Failed to post comment.");
			}

			Browser.Reset();
			OnVideoProcessed(new VideoProcessedEventArgs(commentPostedResult.Video, commentPostedResult.Comment));
		}

		public LoginResult Login()
		{
			var loginBot = new YoutubeLoginBotV1(this, _commentJob.Account, _captchaService);
			var loginResult = loginBot.Run();
			Console.WriteLine(loginResult.Code);
			if (loginResult.Code != LoginResultCode.Success)
			{
				return loginResult;
			}

			return loginResult;
		}

		private void UpdateStatus(string message)
		{
			OnStatusChanged(new StatusChangedEventArgs(message));
		}

		private void ThrowErrorEvent(string message)
		{
			OnError(new FatalErrorEventArgs(message));
		}

		protected virtual void OnError(FatalErrorEventArgs e)
		{
			if (Browser.Proxy?.Address != null)
			{
				var newEvent = new FatalErrorEventArgs("[" + Browser.Proxy.Address.Host + "] - " + e.Error);
				Error?.Invoke(this, newEvent);
			}
			else
			{
				Error?.Invoke(this, e);
			}
		}

		protected virtual void OnStatusChanged(StatusChangedEventArgs e)
		{
			if (Browser.Proxy?.Address != null)
			{
				var newEvent = new StatusChangedEventArgs("[" + Browser.Proxy.Address.Host + "] - " + e.Status);
				StatusChanged?.Invoke(this, newEvent);
			}
			else
			{
				StatusChanged?.Invoke(this, e);
			}
		}

		protected virtual void OnVideoProcessed(VideoProcessedEventArgs e)
		{
			if (Browser.Proxy?.Address != null)
			{
				var newEvent = new VideoProcessedEventArgs(e.Video, "[" + Browser.Proxy.Address.Host + "] - " + e.Comment);
				VideoProcessed?.Invoke(this, newEvent);
			}
			else
			{
				VideoProcessed?.Invoke(this, e);
			}
		}



		public CommentPostedResult Browser_postComment()
		{
			var _commentBot = _botFactory.Create(Browser);
			if (_commentBot.PostComment(_commentJob, 5).Code != PostCommentResultCode.Success)
			{
				return null;
			}

			return new CommentPostedResult(_commentJob.Video, _commentJob.Comment.Value, CommentPostedResultCode.Success);
		}
	}
}