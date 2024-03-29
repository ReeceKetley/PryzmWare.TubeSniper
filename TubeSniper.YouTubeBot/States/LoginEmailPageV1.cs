﻿using System;
using System.Threading;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Extensions;
using TubeSniper.YouTubeBot.Model;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States
{
	class LoginEmailPageV1 : LoginState
	{
		public LoginEmailPageV1(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{
		}

		public void SetEmail(string email)
		{
			_browser.Keyboard.TypeString(email);
		}

		public void Submit()
		{
			_browser.Mouse.MoveToAndClickElement("#next");
		}

		public void SubmitEmail(string email)
		{
			if (TimeoutHelper.Wait(
				    () => _browser.WebView.ElementExists("#Email"),
				    TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}

			Thread.Sleep(100);
			_browser.Mouse.MoveToAndClickElement("#Email");
			_browser.Keyboard.SelectAllBackspace();
			SetEmail(email);
			Thread.Sleep(200);
			Submit();
		}

		public static bool Detect(YoutubeBot browser)
		{
			if (browser.Browser.WebView.ElementExists("#identifier-shown") && browser.Browser.WebView.Url.Contains("accounts.google.com/ServiceLogin?sacu=1#identifier"))
			{
				return true;
			}

			return false;
		}
	}
}