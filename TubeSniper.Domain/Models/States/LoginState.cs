using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Models.States
{
    public abstract class LoginState
    {
        protected readonly VirtualBrowser _browser;

        protected LoginState(VirtualBrowser browser)
        {
            _browser = browser;
        }
    }
}