using System;
using System.Threading;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.Extensions;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V0
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

		public static bool Detect(YoutubeBot browser)
		{
			if (!browser.Browser.WebView.Url.Contains("/challenge/kpe"))
			{
				return false;
			}

			return true;
		}
	}
}