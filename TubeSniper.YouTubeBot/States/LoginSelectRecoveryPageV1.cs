using System;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Extensions;
using TubeSniper.YouTubeBot.Model;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States
{
	public class LoginSelectRecoveryPageV1 : LoginState
	{
		public void SelectRecoveryEmail()
		{
			if (TimeoutHelper.Wait(
				    () => _browser.WebView.ElementExists("#challengePickerList"),
				    TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}
			_browser.Keyboard.PressTab();
			Thread.Sleep(50);
			_browser.Keyboard.KeyPress(KeyCode.Space);
		}

		public void Submit()
		{
			_browser.Keyboard.PressSubmit();
		}

		public LoginSelectRecoveryPageV1(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBot browser)
		{
			if (browser.Browser.WebView.Url.Contains("signin/selectchallenge/"))
			{
				return true;
			}

			return false;
		}
	}
}
