using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V0
{
	public class LoginAccountSuspended : LoginState
	{
		public static bool Detect(YoutubeBrowser browser)
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
