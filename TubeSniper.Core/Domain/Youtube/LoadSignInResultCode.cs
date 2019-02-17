namespace TubeSniper.Core.Domain.Youtube
{
    public enum LoadSignInResultCode
    {
        LoadYoutubeError,
        SignInButtonClickFail,
        Success,
        LoadLoginTimeout,
        AlreadyLoggedIn
    }
}