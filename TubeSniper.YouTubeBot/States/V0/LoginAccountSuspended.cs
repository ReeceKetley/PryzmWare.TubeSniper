using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States.V0
{
	public class LoginAccountSuspended : LoginState
	{
		public static bool Detect(YoutubeBot browser)
		{
			if (!browser.Browser.WebView.Url.Contains("support.google") || !browser.Browser.WebView.Url.Contains("40039"))
			{
				return false;
			}

			return true;
		}

		public LoginAccountSuspended(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{
		}
	}
}
