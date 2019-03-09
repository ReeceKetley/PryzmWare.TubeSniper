using System;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.Extensions;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V0
{
	public class LoginPasswordPage : LoginState
	{
		public LoginPasswordPage(VirtualBrowser browser) : base(browser)
		{
		}

		public void SetPassword(string password)
		{
			if (TimeoutHelper.Wait(
					() => _browser.WebView.ElementExists("input[type =\\\"password\\\"]"),
					TimeSpan.FromSeconds(30)) == WaitCode.Timeout)
			{
				return;
			}

			_browser.Keyboard.TypeString(password);
		}

		public void SetAriaInvalid(string value)
		{
			var code = Globals.SelectorPayload.TypeofJqueryUndefinedInputTypePasswordAttrAriaInvalid + value + "\")";
			_browser.WebView.EvalScript(code);
		}

		public string GetAriaInvalid()
		{
			return (string)_browser.WebView.EvalScript("(typeof jQuery != 'undefined') && $(\"input[type=\\\"password\\\"]\").attr(\"aria-invalid\")");
		}

		public bool IsInvalidPassword()
		{
			return (bool)_browser.WebView.EvalScript("(typeof jQuery != 'undefined') && $(\"input[type=\\\"password\\\"]\").attr(\"aria-invalid\") == \"true\"");
		}

		public void Submit()
		{
			_browser.Keyboard.PressSubmit();
		}

		public static bool Detect(YoutubeBot browser)
		{
			while (!browser.Browser.WebView.CanEvalScript)
			{
			}

			browser.Browser.WebView.InjectJQuery();
			return browser.Browser.WebView.EvalScript(Globals.SelectorPayload.ImgSrcCaptchaLength) != null && browser.Browser.WebView.Url.Contains(Globals.SelectorPayload.SigninV2SlPwd);
		}
	}
}