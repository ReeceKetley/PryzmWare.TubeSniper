using System;
using System.Threading;
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
			Console.WriteLine("Select Recvoery");

			//_browser.Keyboard.PressTab();
			_browser.WebView.ClickElement("#challengePickerList > li:nth-child(1)");
			Thread.Sleep(50);
			//_browser.Keyboard.TypeString(email);
			//Thread.Sleep(50);
			_browser.Keyboard.PressSubmit();
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
			if (browser.Browser.WebView.Url.Contains("signin/selectchallenge/4"))
			{
				Console.WriteLine("Select");
				return true;
			}

			return false;
		}
	}
}
