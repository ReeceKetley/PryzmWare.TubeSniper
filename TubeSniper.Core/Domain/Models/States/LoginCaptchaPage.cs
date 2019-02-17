using System;
using System.Threading;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;
using static TubeSniper.Core.Domain.Auth.SelectorPayload;

namespace TubeSniper.Core.Domain.Models.States
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
	        _browser.WebView.EvalScript(InputTypePasswordEqFocus, true);
	        _browser.Keyboard.SelectAllBackspace();
			_browser.Keyboard.SubmitString(password);
	        Password = password;
			_browser.Keyboard.PressSubmit();

		}

		public string GetImageUrl()
        {
	        
	        var imgSrc = _browser.WebView.GetEvalString(ImgSrcCaptchaAttrSrc);
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
	        _browser.WebView.EvalScript(InputTypeTextEqFocus, true);
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

		public LoginCaptchaPage(VirtualBrowser browser) : base(browser)
        {

        }

        public static bool Detect(YoutubeBrowser browser)
        {
			Console.WriteLine("Captcha page");
	        var imgSrc = browser.Browser.WebView.GetEvalString(ImgSrcCaptchaAttrSrc);
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