using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V0
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

		public LoginAccountNotFound(VirtualBrowser browser) : base(browser)
		{
		}
	}
}
