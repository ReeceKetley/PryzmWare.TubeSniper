using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States
{
	class LoginProxyErrorV1 : LoginState
	{
		public LoginProxyErrorV1(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{
		}

		public static bool Detect(YoutubeBot browser)
		{
			if (browser.Browser.WebView.GetHtml().Contains("The Page Failed to Load"))
			{
				return true;
			}

			return false;
		}
	}
}
