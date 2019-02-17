using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Interfaces
{
    public interface ILoginService
    {
        LoginResult Login(VirtualBrowser browser, YoutubeAccount account);
    }
}
