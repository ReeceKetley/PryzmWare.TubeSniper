using System;
using System.Threading;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Extensions;
using TubeSniper.YouTubeBot.Model;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States.V0
{
	public class LoginCaptchaPage : LoginState
	{
		public string Password;
		public string Token;

		public void SetPassword(string password)
		{
			if (TimeoutHelper.Wait(
					() => _browser.WebView.ElementExists("input[type =\\\"password\\\"]"),
					TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}
			_browser.Keyboard.PressSubmit();
			_browser.WebView.EvalScript(Globals.SelectorPayload.InputTypePasswordEqFocus, true);
			_browser.Keyboard.SelectAllBackspace();
			_browser.Keyboard.SubmitString(password);
			Password = password;
			_browser.Keyboard.PressSubmit();

		}

		public string GetImageUrl()
		{

			var imgSrc = _browser.WebView.GetEvalString("#gaia_loginform > div.form-panel.second > div > div.captcha-container > div > div.captcha-img > img");
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

		public LoginCaptchaPage(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{

		}

		public static bool Detect(YoutubeBot browser)
		{
			Console.WriteLine("Captcha page");
			var imgSrc = browser.Browser.WebView.GetEvalString(Globals.SelectorPayload.ImgSrcCaptchaAttrSrc);
			if (string.IsNullOrEmpty(imgSrc))
			{
				return false;
			}

			if (imgSrc.Length < 5)
			{
				return false;
			}

			return true;
		}
	}
}