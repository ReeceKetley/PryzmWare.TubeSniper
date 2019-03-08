using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models.States.V1
{
    class LoginYoutubeHomeStateV1 : LoginState
    {
        public LoginYoutubeHomeStateV1(VirtualBrowser browser) : base(browser)
        {
        }

        public static bool Detect(YoutubeBrowser browser)
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
