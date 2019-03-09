using System;
using System.Collections.Generic;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces;
using TubeSniper.Domain.Youtube.Extensions;

namespace TubeSniper.Domain.Youtube
{
	public class YoutubeCommentBot : IYoutubeCommentBot
	{
		private readonly VirtualBrowser.VirtualBrowser _browser;

		public YoutubeCommentBot(VirtualBrowser.VirtualBrowser browser)
		{
			_browser = browser;
		}

		public PostCommentResult PostComment(CommentJob commentJob, int watchDuration)
		{
			if (!LoadVideo(commentJob.Video))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}
			_browser.WebView.InjectJQuery();
			if (!LoadComments())
			{
				return new PostCommentResult(PostCommentResultCode.ObjectNotFound);
			}

			if (!ActivateCommentBox(commentJob.CommentMethod))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			if (!TypeComment(commentJob.Comment))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			if (!SubmitComment())
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			return new PostCommentResult(PostCommentResultCode.Success);
		}

		private bool LoadVideo(YoutubeVideo video)
		{
			return _browser.LoadUrlAndVerify("https://m.youtube.com/watch?v=" + video.Id);
		}

		private bool SubmitComment()
		{
			Thread.Sleep(500);
			_browser.Keyboard.PressTab();
			Thread.Sleep(300);
			_browser.Keyboard.PressTab();
			Thread.Sleep(300);
			_browser.Keyboard.KeyPress(KeyCode.Space);
			Thread.Sleep(3000);
			return true;
		}

		private bool LoadComments()
		{
			var endTime = DateTime.Now + TimeSpan.FromSeconds(30);
			for (;; Thread.Sleep(250))
			{
				if (DateTime.Now > endTime)
				{
					return false;
				}

				if (!_browser.WebView.ElementExists(".cbox"))
				{
					continue;
				}

				return true;
			}
		}

		private bool ActivateCommentBox(CommentMethod commentMethod)
		{
			if (commentMethod == CommentMethod.Comment)
			{
				if (!MoveToCommentBox())
				{
					return false;
				}
			}
			else
			{
				if (!ClickReplyToTopComment())
				{
					return false;
				}
			}

			return true;
		}

		private bool ClickReplyToTopComment()
		{
			_browser.WebView.EvalScript("$(\".comment-text.user-text\").click();", true);
			Thread.Sleep(200);
			_browser.WebView.EvalScript("$(\"#menu button\").click()", true);
			Thread.Sleep(200);
			_browser.WebView.EvalScript("$(\".comment-reply-input\").focus()", true);
			return true;
		}

		private bool MoveToCommentBox()
		{
			_browser.WebView.EvalScript("$(\"ytm-comment-section-header-renderer\").click();", true);
			Thread.Sleep(200);
			_browser.WebView.EvalScript("$(\".comment-simplebox-reply\").click();", true);
			return true;
		}

		private bool TypeComment(Comment comment)
		{
			Thread.Sleep(500);
			_browser.Keyboard.TypeString(comment.Value);
			Thread.Sleep(1000);
			return true;
		}
	}
}