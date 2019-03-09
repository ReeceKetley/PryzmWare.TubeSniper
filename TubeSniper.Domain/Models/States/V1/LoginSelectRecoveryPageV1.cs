using System;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.Extensions;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V1
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

		public LoginSelectRecoveryPageV1(VirtualBrowser browser) : base(browser)
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
