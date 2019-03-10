using System;
using System.Threading;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Extensions;
using TubeSniper.YouTubeBot.Model;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States
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

		public LoginPasswordPageV1(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBot browser)
		{
			if (browser.Browser.WebView.ElementExists("#gaia_loginform") && browser.Browser.WebView.Url.Contains("#password") && !browser.Browser.WebView.Url.Contains("challenge"))
			{
				return true;
			}

			return false;
		}
	}
}
