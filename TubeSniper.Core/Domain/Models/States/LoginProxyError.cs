using System;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States
{
	class LoginProxyError : LoginState
	{
		public LoginProxyError(VirtualBrowser browser) : base(browser)
		{
		}

		public static bool Detect(YoutubeBrowser browser)
		{
			Console.WriteLine("Proxy Detction");
			if (browser.Browser.WebView.Url.Contains("about") && !browser.Browser.WebView.GetHtml().Contains("google"))
			{
				Console.WriteLine("Proxy Dead");
				return false;
			}

			return true;
		}
	}
}
