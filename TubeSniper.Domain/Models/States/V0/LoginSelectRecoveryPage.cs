using System;
using System.Threading;
using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.Extensions;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V0
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

        public static bool Detect(YoutubeBot browser)
        {
            if (!browser.Browser.WebView.Url.Contains("/challenge/selection"))
            {
                return false;
            }

            return true;
        }
    }
}