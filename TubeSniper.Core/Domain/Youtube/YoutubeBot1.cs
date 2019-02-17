using System;
using System.Net;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Youtube
{
	public class YoutubeBot1
	{
		private readonly YoutubeAccount _account;
		private readonly string _videoSeedId;
		private readonly string _comment;
		private readonly YoutubeBrowser _browser;
		public event EventHandler<FatalErrorEventArgs> Error;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		public YoutubeBot1(YoutubeAccount account, WebProxy proxy, string videoSeedId, string comment, bool useCache)
		{
			_account = account;
			_videoSeedId = videoSeedId;
			_comment = comment;
			_browser = new YoutubeBrowser(proxy, account, useCache);
		}

		public void Run()
		{
			UpdateStatus("Logging in: " + _account.Credentials.Email);
			var loginResult = Login().Code;
			if (loginResult != LoginResultCode.Success)
			{
				ThrowErrorEvent("Failed to login to account: + " + _account.Credentials.Email + ". (" + loginResult + ")");
				return;
			}

			UpdateStatus("Logged in: " + _account.Credentials.Email);
			UpdateStatus("Posting Comment: " + _videoSeedId);
			var commentPostedResult = _browser.PostComment(_browser, _videoSeedId, _comment, false);
			if (commentPostedResult.Code != CommentPostedResultCode.Success)
			{
				ThrowErrorEvent("Comment post failure");
				UpdateStatus("Failed to post comment.");
			}

			_browser.Browser.WebView.Engine.Stop(false);
			_browser.Browser.WebView.Destroy();
			OnVideoProcessed(new VideoProcessedEventArgs(commentPostedResult.MetaData, _comment));
		}

		public LoginResult Login()
		{
			var loginBot = new YoutubeLoginBot(_browser, _account);
			var loginResult = loginBot.Run();
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
			Error?.Invoke(this, e);
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