using System;
using System.Net;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Youtube
{
	public class YoutubeBot
	{
		private readonly YoutubeAccount _account;
		private readonly string _videoSeedId;
		private readonly string _comment;
		private readonly bool _asReply;
		public readonly YoutubeBrowser _browser;
		public event EventHandler<FatalErrorEventArgs> Error;
		public event EventHandler<StatusChangedEventArgs> StatusChanged;
		public event EventHandler<VideoProcessedEventArgs> VideoProcessed;

		public YoutubeBot(YoutubeAccount account, WebProxy proxy, string videoSeedId, string comment, bool useCache, bool asReply)
		{
			_account = account;
			_videoSeedId = videoSeedId;
			_comment = comment;
			_asReply = asReply;
			_browser = new YoutubeBrowser(proxy, account, useCache);
		}

		public void Run()
		{
			Console.WriteLine("Run");
			if (_account == null)
			{
				return;
			}
			UpdateStatus("Logging in: " + _account.Credentials.Email);
			var loginResult = Login().Code;
			if (loginResult != LoginResultCode.Success)
			{
				ThrowErrorEvent("Failed to login to account: + " + _account.Credentials.Email + ". (" + loginResult + ")");
				SharedData.OnCurrentStep(CurrentStepEventArgs.LogingIn);
				return;
			}

			UpdateStatus("Logged in: " + _account.Credentials.Email);
			SharedData.OnCurrentStep(CurrentStepEventArgs.LoggedIn);
			UpdateStatus("Posting Comment: " + _videoSeedId);
			SharedData.OnCurrentStep(CurrentStepEventArgs.Commenting);
			var commentPostedResult = _browser.PostComment(_browser, _videoSeedId, _comment, _asReply);
			if (commentPostedResult.Code != CommentPostedResultCode.Success)
			{
				SharedData.OnCurrentStep(CurrentStepEventArgs.Failure);
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
				var newEvent = new StatusChangedEventArgs("["+_browser.Proxy.Address.Host + "] - " + e.Status);
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
				var newEvent = new VideoProcessedEventArgs(e.Meta, "[" + _browser.Proxy.Address.Host + "] - " + e.Comment);
				VideoProcessed?.Invoke(this, newEvent);
			}
			else
			{
				VideoProcessed?.Invoke(this, e);
			}
		}
	}
}