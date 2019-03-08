using System;
using System.Collections.Generic;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces;

namespace TubeSniper.Core.Services
{
	public class YoutubeCommentBot : ICommentBot
	{
		public PostCommentResult PostComment(VirtualBrowser browser, string message, bool asReply, int watchDuration)
		{
			browser.WebView.InjectJQuery();
			if (!LoadComments(browser))
			{
				return new PostCommentResult(PostCommentResultCode.ObjectNotFound);
			}

			if (!ActivateCommentBox(browser, asReply))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			if (!TypeComment(browser, message))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			if (!SubmitComment(browser, asReply))
			{
				return new PostCommentResult(PostCommentResultCode.Failure);
			}

			return new PostCommentResult(PostCommentResultCode.Success);
		}

		private bool SubmitComment(VirtualBrowser browser, bool asReply)
		{
			Thread.Sleep(500);
			browser.Keyboard.PressTab();
			Thread.Sleep(300);
			browser.Keyboard.PressTab();
			Thread.Sleep(300);
			browser.Keyboard.KeyPress(KeyCode.Space);
			Thread.Sleep(3000);
			return true;
//			if (asReply)
//			{
//				browser.WebView.GetEvalString("$(\"c3-dialog button\").eq(1).click();");
//			}
//			else
//			{
//				browser.WebView.GetEvalString("$(\".c3-material-button-button .cbox .button-renderer-text\").eq(7).click();");
//			}
//
//			return true;
		}

		private bool LoadComments(VirtualBrowser browser)
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

		private bool ActivateCommentBox(VirtualBrowser browser, bool asReply)
		{
			if (!asReply)
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

		private bool ClickReplyToTopComment(VirtualBrowser browser)
		{
			browser.WebView.EvalScript("$(\".comment-text.user-text\").click();", true);
			Thread.Sleep(200);
			browser.WebView.EvalScript("$(\"#menu button\").click()", true);
			Thread.Sleep(200);
			browser.WebView.EvalScript("$(\".comment-reply-input\").focus()", true);
			return true;
		}

		private bool MoveToCommentBox(VirtualBrowser browser)
		{
			browser.WebView.EvalScript("$(\"ytm-comment-section-header-renderer\").click();", true);
			Thread.Sleep(200);
			browser.WebView.EvalScript("$(\".comment-simplebox-reply\").click();", true);
			return true;
		}

		private bool TypeComment(VirtualBrowser browser, string message)
		{
			var template = CommentTemplate.FromString(message);
			Thread.Sleep(500);
			browser.Keyboard.TypeString(template.Generate(new Dictionary<string, string>()));
			Thread.Sleep(1000);
			return true;
		}
	}
}