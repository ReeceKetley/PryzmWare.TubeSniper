using System;
using System.Net;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Domain.Youtube
{
	public class YoutubeBot
	{
		private readonly YoutubeAccount _account;
		private readonly YoutubeVideo _video;
		private readonly string _comment;
		private readonly bool _asReply;
		public readonly YoutubeBrowser _browser;
		public event EventHandler<FatalErrorEventArgs> Error;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		public YoutubeBot(YoutubeAccount account, HttpProxy proxy, YoutubeVideo video, string comment, bool useCache, bool asReply)
		{
			_account = account;
			_video = video;
			_comment = comment;
			_asReply = asReply;
			_browser = new YoutubeBrowser(proxy, account, useCache);
		}

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
			var commentPostedResult = _browser.PostComment(_browser, _video, _comment, _asReply);
			if (commentPostedResult.Code != CommentPostedResultCode.Success)
			{
				SharedData.OnCurrentStep(CurrentStepEventArgs.Failure);
				ThrowErrorEvent("Comment post failure");
				UpdateStatus("Failed to post comment.");
			}

			_browser.Browser.WebView.Engine.Stop(false);
			_browser.Browser.WebView.Destroy();
			OnVideoProcessed(new VideoProcessedEventArgs(commentPostedResult.Video, _comment));
		}

		public LoginResult Login()
		{
			var loginBot = new YoutubeLoginBotV1(_browser, _account);
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
			if (_browser.Proxy?.Address != null)
			{
				var newEvent = new FatalErrorEventArgs("[" + _browser.Proxy.Address.Host + "] - " + e.Error);
				Error?.Invoke(this, newEvent);
			}
			else
			{
				Error?.Invoke(this, e);
			}
		}

		protected virtual void OnStatusChanged(StatusChangedEventArgs e)
		{
			if (_browser.Proxy?.Address != null)
			{
				var newEvent = new StatusChangedEventArgs("[" + _browser.Proxy.Address.Host + "] - " + e.Status);
				StatusChanged?.Invoke(this, newEvent);
			}
			else
			{
				StatusChanged?.Invoke(this, e);
			}
		}

		protected virtual void OnVideoProcessed(VideoProcessedEventArgs e)
		{
			if (_browser.Proxy?.Address != null)
			{
				var newEvent = new VideoProcessedEventArgs(e.Video, "[" + _browser.Proxy.Address.Host + "] - " + e.Comment);
				VideoProcessed?.Invoke(this, newEvent);
			}
			else
			{
				VideoProcessed?.Invoke(this, e);
			}
		}
	}
}