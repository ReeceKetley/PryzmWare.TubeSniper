using System;
using System.Threading;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States
{
    public class LoginSelectRecoveryPage : LoginState
    {
        public void ClickEmail()
        {
            _browser.WebView.ElementExists("div[role='button']", TimeSpan.FromSeconds(30));
			//_browser.Keyboard.PressTab();
			Thread.Sleep(75);
			//_browser.Keyboard.PressSubmit();
            _browser.WebView.EvalScript("$(\"div[role='button']\").eq(0).click()");
        }

        public LoginSelectRecoveryPage(VirtualBrowser browser) : base(browser)
        {

        }

        public static bool Detect(YoutubeBrowser browser)
        {
            if (!browser.Browser.WebView.Url.Contains("/challenge/selection"))
            {
                return false;
            }

            return true;
        }
    }
}