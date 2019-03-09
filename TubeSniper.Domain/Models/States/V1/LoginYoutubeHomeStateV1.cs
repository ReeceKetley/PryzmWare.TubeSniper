using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States.V1
{
    class LoginYoutubeHomeStateV1 : LoginState
    {
        public LoginYoutubeHomeStateV1(VirtualBrowser browser) : base(browser)
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
