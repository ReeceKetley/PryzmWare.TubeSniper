using TubeSniper.Domain.Youtube;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Interfaces
{
    public interface ILoginService
    {
        LoginResult Login(VirtualBrowser browser, YoutubeAccount account);
    }
}
