using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States.V0
{
    class LoginYoutubeHomeState : LoginState
    {
        public LoginYoutubeHomeState(VirtualBrowser.VirtualBrowser browser) : base(browser)
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
