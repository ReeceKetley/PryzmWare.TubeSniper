using System;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Comments;

namespace TubeSniper.YouTubeBot.Youtube
{
	public class YoutubeBot
	{
		private readonly CommentJob _commentJob;
		public readonly VirtualBrowser.VirtualBrowser Browser;

		public event EventHandler<CaptchaReceivedEventArgs> CaptchaReceived;

		public YoutubeBot(CommentJob commentJob, string cachePath)
		{
			_commentJob = commentJob;
			Browser = VirtualBrowser.VirtualBrowser.Create(commentJob.Proxy, cachePath);
		}

		public void Run()
		{
			var loginResult = Login();
			Console.WriteLine(loginResult.Code);

			if (loginResult.Code != LoginResultCode.Success)
			{
				Browser.Reset();
				// TODO: return error info
				return;
			}

			var commentPostedResult = PostComment();
			if (commentPostedResult.Code != CommentPostedResultCode.Success)
			{
				Browser.Reset();
				// TODO: return error info
				return;
			}

			Browser.Reset();
			// comment ID
			// return success info
		}

		private LoginResult Login()
		{
			var loginBot = new YoutubeLoginBotV1(this, _commentJob.Account);
			var loginResult = loginBot.Run();
			Console.WriteLine(loginResult.Code);
			if (loginResult.Code != LoginResultCode.Success)
			{
				return loginResult;
			}

			return loginResult;
		}

		public CommentPostedResult PostComment()
		{
			var commentBot = new YoutubeCommentBot(Browser);
			var postCommentResult = commentBot.PostComment(_commentJob, 5);
			Console.WriteLine(postCommentResult.Code);
			if (postCommentResult.Code != PostCommentResultCode.Success)
			{
				return null;
			}

			return new CommentPostedResult(_commentJob.VideoId, _commentJob.Comment.Value, CommentPostedResultCode.Success);
		}


		public string SolveCaptcha(Uri imageUri)
		{
			var e = new CaptchaReceivedEventArgs(imageUri);
			OnCaptchaReceived(e);
			// TODO: handle no solution?
			return e.Solution;
		}

		protected virtual void OnCaptchaReceived(CaptchaReceivedEventArgs e)
		{
			CaptchaReceived?.Invoke(this, e);
		}
	}
}