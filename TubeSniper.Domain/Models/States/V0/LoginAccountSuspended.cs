using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V0
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

		public LoginAccountSuspended(VirtualBrowser browser) : base(browser)
		{
		}
	}
}
