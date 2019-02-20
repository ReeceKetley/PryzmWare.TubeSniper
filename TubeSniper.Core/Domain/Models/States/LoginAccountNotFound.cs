using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States
{
	public class LoginAccountNotFound : LoginState
	{
		public static bool Detect(YoutubeBrowser browser)
		{
			if (!browser.Browser.WebView.GetHtml().Contains("</path></svg></span>Couldn\'t find your Google Account</div>"))
			{
				return false;
			}
			return true;
		}

		public LoginAccountNotFound(VirtualBrowser browser) : base(browser)
		{
		}
	}
}
