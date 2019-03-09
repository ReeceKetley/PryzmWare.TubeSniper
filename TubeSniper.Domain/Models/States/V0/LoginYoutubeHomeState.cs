using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V0
{
    class LoginYoutubeHomeState : LoginState
    {
        public LoginYoutubeHomeState(VirtualBrowser browser) : base(browser)
        {
        }

        public static bool Detect(YoutubeBot browser)
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
