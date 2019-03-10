namespace TubeSniper.YouTubeBot.Auth
{
    public abstract class LoginState
    {
        protected readonly VirtualBrowser.VirtualBrowser _browser;

        protected LoginState(VirtualBrowser.VirtualBrowser browser)
        {
            _browser = browser;
        }
    }
}