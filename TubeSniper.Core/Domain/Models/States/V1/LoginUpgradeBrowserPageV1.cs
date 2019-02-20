using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V1
{
	class LoginUpgradeBrowserPageV1 : LoginState
	{
		public void SetUserAgent(YoutubeBrowser browser)
		{
			browser.Browser.WebView.LoadUrlAndWait("https://www.youtube.com");
			browser.Browser.WebView.CustomUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:65.0) Gecko/20100101 Firefox/65.0";
			browser.Browser.WebView.LoadUrlAndWait("https://www.youtube.com");
			browser.Browser.WebView.LoadUrlAndWait("https://recaptcha-demo.appspot.com/recaptcha-v3-request-scores.php");
		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (browser.Browser.WebView.Url.Contains("myaccount.google.com/general-light"))
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
