using System;
using System.Threading;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Extensions;
using TubeSniper.YouTubeBot.Model;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States.V0
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

		public LoginSubmitRecoveryPage(VirtualBrowser.VirtualBrowser browser) : base(browser)
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