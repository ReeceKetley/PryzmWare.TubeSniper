using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States
{
	class LoginCreateChannelPageV1 : LoginState
	{
		public void Submit()
		{
			_browser.WebView.EvalScript("$(\".yt-google-name-button\").click()");
		}

		public LoginCreateChannelPageV1(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBot browser)
		{
			if (browser.Browser.WebView.Url.Contains("https://m.youtube.com/create_channel"))
			{
				return true;
			}

			return false;
		}
	}
}
