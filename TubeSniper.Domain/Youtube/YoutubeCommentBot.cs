using System;
using System.Collections.Generic;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Domain.Interfaces;
using TubeSniper.Domain.Youtube.Extensions;

namespace TubeSniper.Domain.Youtube
{
	public class YoutubeCommentBot
	{
		private readonly VirtualBrowser.VirtualBrowser _browser;

		public YoutubeCommentBot(VirtualBrowser.VirtualBrowser browser)
		{
			_browser = browser;
		}

		public PostCommentResult PostComment(YoutubeVideo video, string message, CommentMethod commentMethod, int watchDuration)
		{
			if (!LoadVideoById(video.Id))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}
			_browser.WebView.InjectJQuery();
			if (!LoadComments(_browser))
			{
				return new PostCommentResult(PostCommentResultCode.ObjectNotFound);
			}

			if (!ActivateCommentBox(_browser, commentMethod))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			if (!TypeComment(_browser, message))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			if (!SubmitComment(_browser))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			return new PostCommentResult(PostCommentResultCode.Success);
		}

		private bool LoadVideoById(string videoId)
		{
			return _browser.LoadUrlAndVerify("https://m.youtube.com/watch?v=" + videoId);
		}

		private bool SubmitComment(VirtualBrowser.VirtualBrowser browser)
		{
			Thread.Sleep(500);
			browser.Keyboard.PressTab();
			Thread.Sleep(300);
			browser.Keyboard.PressTab();
			Thread.Sleep(300);
			browser.Keyboard.KeyPress(KeyCode.Space);
			Thread.Sleep(3000);
			return true;
		}

		private bool LoadComments(VirtualBrowser.VirtualBrowser browser)
		{
			var endTime = DateTime.Now + TimeSpan.FromSeconds(30);
			for (;; Thread.Sleep(250))
			{
				if (DateTime.Now > endTime)
				{
					return false;
				}

				if (!browser.WebView.ElementExists(".cbox"))
				{
					continue;
				}

				return true;
			}
		}

		private bool ActivateCommentBox(VirtualBrowser.VirtualBrowser browser, CommentMethod commentMethod)
		{
			if (commentMethod == CommentMethod.Comment)
			{
				if (!MoveToCommentBox(browser))
				{
					return false;
				}
			}
			else
			{
				if (!ClickReplyToTopComment(browser))
				{
					return false;
				}
			}

			return true;
		}

		private bool ClickReplyToTopComment(VirtualBrowser.VirtualBrowser browser)
		{
			browser.WebView.EvalScript("$(\".comment-text.user-text\").click();", true);
			Thread.Sleep(200);
			browser.WebView.EvalScript("$(\"#menu button\").click()", true);
			Thread.Sleep(200);
			browser.WebView.EvalScript("$(\".comment-reply-input\").focus()", true);
			return true;
		}

		private bool MoveToCommentBox(VirtualBrowser.VirtualBrowser browser)
		{
			browser.WebView.EvalScript("$(\"ytm-comment-section-header-renderer\").click();", true);
			Thread.Sleep(200);
			browser.WebView.EvalScript("$(\".comment-simplebox-reply\").click();", true);
			return true;
		}

		private bool TypeComment(VirtualBrowser.VirtualBrowser browser, string message)
		{
			Thread.Sleep(500);
			browser.Keyboard.TypeString(message);
			Thread.Sleep(1000);
			return true;
		}
	}
}