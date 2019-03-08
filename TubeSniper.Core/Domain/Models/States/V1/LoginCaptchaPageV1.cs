using System;
using System.Threading;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V1
{
	class LoginCaptchaPageV1 : LoginState
	{
		public string Password;
		public string Token;

		public void SetPassword(string password)
		{
			if (TimeoutHelper.Wait(
				    () => _browser.WebView.ElementExists("#Passwd"),
				    TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}
			_browser.Mouse.MoveToAndClickElement("#Passwd");
			Thread.Sleep(50);
			_browser.Keyboard.SelectAllBackspace();
			Thread.Sleep(25);
			_browser.Keyboard.TypeString(password);
		}

		public string GetImageUrl()
		{
			var imgSrc = _browser.WebView.GetEvalString("$(\"[name=\'url_audio\']\").attr(\"value\")").Replace("&kind=audio", "");
			if (string.IsNullOrEmpty(imgSrc.Trim()))
			{ 
				return null;
			}

			return imgSrc;
		}

		public void SetToken(string token)
		{
			_browser.Keyboard.PressTab();
			Thread.Sleep(25);
			_browser.Keyboard.PressTab();
			Token = token;
			Thread.Sleep(25);
			_browser.Keyboard.SelectAllBackspace();
			Thread.Sleep(25);
			_browser.Keyboard.TypeString(token);
		}

		public void Submit()
		{
			_browser.Mouse.MoveToAndClickElement("#signIn");
			Thread.Sleep(1000);
		}

		public LoginCaptchaPageV1(VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (browser.Browser.WebView.Url.Contains("challenge") && browser.Browser.WebView.GetHtml().Contains("<img src=\"https://accounts.google.com/Captcha?v=2"))
			{
				return true;
			}
			return false;
		}
	}
}

