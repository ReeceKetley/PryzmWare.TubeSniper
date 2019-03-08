using System;
using System.Threading;
using EO.WebBrowser;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V1
{
	public class LoginSubmitRecoveryPageV1 : LoginState
	{
		public void SetRecoveryEmail(string email)
		{
			if (TimeoutHelper.Wait(
				    () => _browser.WebView.ElementExists("#challenge > content > div > div:nth-child(3) > input"),
				    TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}

			_browser.Keyboard.TypeString(email);
			Thread.Sleep(250);
			Console.WriteLine("Clicking submit");
			_browser.Mouse.MoveToAndClickElement("#submit");
			_browser.Keyboard.KeyPress(KeyCode.Enter);
		}

		public void Submit()
		{
			_browser.Keyboard.KeyPress(KeyCode.Enter);
		}

		public LoginSubmitRecoveryPageV1(VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (browser.Browser.WebView.Url.Contains("accounts.google.com/signin/challenge/kpe/4"))
			{
				return true;
			}

			return false;
		}
	}
}
