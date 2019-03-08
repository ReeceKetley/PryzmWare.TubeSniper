using System;
using System.Threading;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V0
{
	public class LoginEmailPage : LoginState
	{
		public LoginEmailPage(VirtualBrowser browser) : base(browser)
		{
		}

		public void SetEmail(string email)
		{
			_browser.Keyboard.TypeString(email);
		}

		public void Submit()
		{
			_browser.Keyboard.PressSubmit();
		}

		public void SubmitEmail(string email)
		{
			if (TimeoutHelper.Wait(
					() => _browser.WebView.ElementExists(Globals.SelectorPayload.LoginEmailidentifierid),
					TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}

			Thread.Sleep(100);
			_browser.Keyboard.SelectAllBackspace();
			SetEmail(email);
			Thread.Sleep(200);
			Submit();
		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (!browser.Browser.WebView.Url.Contains(Globals.SelectorPayload.SigninV2Identifier))
			{
				return false;
			}

			return true;
		}
	}
}