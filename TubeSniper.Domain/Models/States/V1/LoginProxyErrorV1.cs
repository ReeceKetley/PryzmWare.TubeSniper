using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V1
{
	class LoginProxyErrorV1 : LoginState
	{
		public LoginProxyErrorV1(VirtualBrowser browser) : base(browser)
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
