using System.Net;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Interfaces;

namespace TubeSniper.Core.Domain.Youtube
{
	public class YoutubeBrowser
	{
		private ILoginService _loginService;
		private YoutubeVideoPage _videoPage;

		public YoutubeBrowser(WebProxy proxy, YoutubeAccount account, bool useCache = false)
		{
			Proxy = proxy;
			Browser = VirtualBrowser.Create(proxy, account, useCache);
		}

		public VirtualBrowser Browser { get; }

		public WebProxy Proxy { get; }

		public LoginResultCode Login(YoutubeAccount credentials)
		{
			var loginResult = _loginService.Login(Browser, credentials).Code;
			return loginResult;
		}

		public bool LoadVideoById(string videoId)
		{
			return Browser.LoadUrlAndVerify("https://www.youtube.com/watch?v=" + videoId);
		}

		public CommentPostedResult PostComment(YoutubeBrowser browser, YoutubeVideo video, string comment, bool asReply)
		{
			_videoPage = new YoutubeVideoPage(browser);
			if (!LoadVideoById(video.Id))
			{
				return null;
			}

			if (!_videoPage.PostComment(comment, asReply))
			{
				return new CommentPostedResult(video, comment, CommentPostedResultCode.Failure);
			}

			return new CommentPostedResult(video, comment, CommentPostedResultCode.Success);
		}
	}
}