using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.Model
{
    public interface ILoginService
    {
        LoginResult Login(VirtualBrowser.VirtualBrowser browser, YoutubeAccount account);
    }
}
