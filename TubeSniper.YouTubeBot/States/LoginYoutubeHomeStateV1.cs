using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.States
{
    class LoginYoutubeHomeStateV1 : LoginState
    {
        public LoginYoutubeHomeStateV1(VirtualBrowser.VirtualBrowser browser) : base(browser)
        {
        }

        public static bool Detect(YoutubeBot browser)
        {
            //Console.WriteLine(browser.Browser.WebView.Url);
            if (!browser.Browser.WebView.Url.Contains("youtube.com/"))
            {
                return false;
            }

            return true;
        }
    }
}
