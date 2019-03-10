using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States.V0
{
	public class LoginAccountNotFound : LoginState
	{
		public static bool Detect(YoutubeBot browser)
		{
			if (!browser.Browser.WebView.GetHtml().Contains("</path></svg></span>Couldn\'t find your Google Account</div>"))
			{
				return false;
			}
			return true;
		}

		public LoginAccountNotFound(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{
		}
	}
}
