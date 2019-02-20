using System;
using System.Threading;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V1
{
	public class LoginPasswordPageV1 : LoginState
	{
		public void SetPassword(string password)
		{
			if (TimeoutHelper.Wait(
				    () => _browser.WebView.ElementExists("#password-shown"),
				    TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}
			_browser.Mouse.MoveToAndClickElement("#Passwd");
			Thread.Sleep(100);
			_browser.Keyboard.TypeString(password);
			Thread.Sleep(50);
		}

		public void Submit()
		{
			_browser.Mouse.MoveToAndClickElement("#signIn");
		}

		public LoginPasswordPageV1(VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (browser.Browser.WebView.ElementExists("#gaia_loginform") && browser.Browser.WebView.Url.Contains("accounts.google.com/ServiceLogin?sacu=1#password"))
			{
				return true;
			}

			return false;
		}
	}
}
