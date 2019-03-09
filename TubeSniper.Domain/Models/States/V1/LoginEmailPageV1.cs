using System;
using System.Threading;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.Extensions;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V1
{
	class LoginEmailPageV1 : LoginState
	{
		public LoginEmailPageV1(VirtualBrowser browser) : base(browser)
		{
		}

		public void SetEmail(string email)
		{
			_browser.Keyboard.TypeString(email);
		}

		public void Submit()
		{
			_browser.Mouse.MoveToAndClickElement("#next");
		}

		public void SubmitEmail(string email)
		{
			if (TimeoutHelper.Wait(
				    () => _browser.WebView.ElementExists("#Email"),
				    TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}

			Thread.Sleep(100);
			_browser.Mouse.MoveToAndClickElement("#Email");
			_browser.Keyboard.SelectAllBackspace();
			SetEmail(email);
			Thread.Sleep(200);
			Submit();
		}

		public static bool Detect(YoutubeBot browser)
		{
			if (browser.Browser.WebView.ElementExists("#identifier-shown") && browser.Browser.WebView.Url.Contains("accounts.google.com/ServiceLogin?sacu=1#identifier"))
			{
				return true;
			}

			return false;
		}
	}
}