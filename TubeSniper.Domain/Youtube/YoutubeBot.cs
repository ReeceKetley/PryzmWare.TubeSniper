using System;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;

namespace TubeSniper.Domain.Youtube
{
	public class YoutubeBot
	{
		private readonly YoutubeAccount _account;
		private readonly YoutubeVideo _video;
		private readonly string _comment;
		private readonly CommentMethod _commentMethod;
		private readonly ICaptchaService _captchaService;
		public readonly VirtualBrowser.VirtualBrowser Browser;
		public event EventHandler<FatalErrorEventArgs> Error;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		public YoutubeBot(YoutubeAccount account, HttpProxy proxy, YoutubeVideo video, string comment, bool useCache, bool asReply, ICaptchaService captchaService)
		{
			_account = account;
			_video = video;
			_comment = comment;
			_commentMethod = asReply ? CommentMethod.Reply : CommentMethod.Comment;
			_captchaService = captchaService;
			Proxy = proxy;
			Browser = VirtualBrowser.VirtualBrowser.Create(proxy.ToWebProxy(), account, useCache);
		}

		public HttpProxy Proxy { get; set; }

		public void Run()
		{
			if (_account == null)
			{
				return;
			}
			UpdateStatus("Logging in: " + _account.Credentials.Email);
			var loginResult = Login().Code;
			if (loginResult != LoginResultCode.Success)
			{
				ThrowErrorEvent("Failed to login to account: + " + _account.Credentials.Email + ". (" + loginResult + ")");
			}

			UpdateStatus("Logged in: " + _account.Credentials.Email);
			UpdateStatus("Posting Comment: " + _video);

			var commentPostedResult = Browser_postComment(Browser, _video, _comment);
			if (commentPostedResult.Code != CommentPostedResultCode.Success)
			{
				ThrowErrorEvent("Comment post failure");
				UpdateStatus("Failed to post comment.");
			}

			Browser.Reset();
			OnVideoProcessed(new VideoProcessedEventArgs(commentPostedResult.Video, _comment));
		}

		public LoginResult Login()
		{
			var loginBot = new YoutubeLoginBotV1(this, _account, _captchaService);
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



		public CommentPostedResult Browser_postComment(VirtualBrowser.VirtualBrowser browser, YoutubeVideo video, string comment)
		{
			var _commentBot = new YoutubeCommentBot(browser);
			if (_commentBot.PostComment(video, comment, _commentMethod, 5).Code != PostCommentResultCode.Success)
			{
				return null;
			}

			return new CommentPostedResult(video, comment, CommentPostedResultCode.Success);
		}
	}
}