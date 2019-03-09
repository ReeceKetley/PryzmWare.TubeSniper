using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V1
{
	class LoginUpgradeBrowserPageV1 : LoginState
	{
		public void SetUserAgent(YoutubeBot browser)
		{
			browser.Browser.WebView.LoadUrlAndWait("https://www.youtube.com");
			//browser.Browser.WebView.LoadUrlAndWait("https://recaptcha-demo.appspot.com/recaptcha-v3-request-scores.php");
		}

		public static bool Detect(YoutubeBot browser)
		{
			if (browser.Browser.WebView.Url.Contains("general-light"))
			{
				return true;
			}

			return false;
		}

		public LoginUpgradeBrowserPageV1(VirtualBrowser browser) : base(browser)
		{
		}
	}
}
