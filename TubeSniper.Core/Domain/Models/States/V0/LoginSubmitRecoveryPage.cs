using System;
using System.Threading;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V0
{
	public class LoginSubmitRecoveryPage : LoginState
	{
		public void SetRecoveryEmail(string email)
		{
			if (TimeoutHelper.Wait(
					() => _browser.WebView.ElementExists("#identifierId"),
					TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}

			//_browser.WebView.ClickElement("#identifierId");
			Thread.Sleep(250);
			_browser.Keyboard.TypeString(email);
		}

		public void Submit()
		{
			_browser.Keyboard.PressSubmit();
		}

		public LoginSubmitRecoveryPage(VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (!browser.Browser.WebView.Url.Contains("/challenge/kpe"))
			{
				return false;
			}

			return true;
		}
	}
}