using TubeSniper.Core.Domain.Browser;

namespace TubeSniper.Core.Domain.Models.States
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