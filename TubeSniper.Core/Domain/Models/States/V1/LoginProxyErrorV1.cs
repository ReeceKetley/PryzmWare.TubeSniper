using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V1
{
	class LoginProxyErrorV1 : LoginState
	{
		public LoginProxyErrorV1(VirtualBrowser browser) : base(browser)
		{
		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (browser.Browser.WebView.GetHtml().Contains("The Page Failed to Load"))
			{
				return true;
			}

			return false;
		}
	}
}
