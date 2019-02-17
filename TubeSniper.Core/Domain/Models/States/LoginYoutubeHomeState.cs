using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States
{
    class LoginYoutubeHomeState : LoginState
    {
        public LoginYoutubeHomeState(VirtualBrowser browser) : base(browser)
        {
        }

        public static bool Detect(YoutubeBrowser browser)
        {
            //Console.WriteLine(browser.Browser.WebView.Url);
            if (!browser.Browser.WebView.Url.Contains("https//www.youtube.com/"))
            {
                return false;
            }

            return true;
        }
    }
}
