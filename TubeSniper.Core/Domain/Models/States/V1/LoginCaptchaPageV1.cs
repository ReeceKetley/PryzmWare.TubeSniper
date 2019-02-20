using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
				    () => _browser.WebView.ElementExists("#captcha-container"),
				    TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}

			_browser.Mouse.MoveToAndClickElement(".password-shown");
			Thread.Sleep(50);
		}

		public string GetImageUrl()
		{

			var imgSrc = _browser.WebView.GetEvalString(Globals.SelectorPayload.ImgSrcCaptchaAttrSrc);
			if (string.IsNullOrEmpty(imgSrc.Trim()))
			{
				return null;
			}

			imgSrc = "https://accounts.google.com" + imgSrc;
			return imgSrc;
		}

		public void SetToken(string token)
		{
			Console.WriteLine("- Setting token");
			Token = token;
			_browser.WebView.EvalScript(Globals.SelectorPayload.InputTypeTextEqFocus, true);
			_browser.Keyboard.SelectAllBackspace();
			_browser.Keyboard.TypeString(token);
		}

		public void Submit()
		{
			Console.WriteLine("- Submiting");
			Console.WriteLine(Token);
			_browser.Keyboard.PressSubmit();
			Thread.Sleep(1000);
		}

		public LoginCaptchaPageV1(VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBrowser browser)
		{
			if (browser.Browser.WebView.ElementExists("div[class*=\'main-content\']"))
			{
				return true;
			}
			return false;
		}
	}
}

