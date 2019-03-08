using System;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V1
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

		public static bool Detect(YoutubeBrowser browser)
		{
			if (browser.Browser.WebView.Url.Contains("signin/selectchallenge/"))
			{
				return true;
			}

			return false;
		}
	}
}
