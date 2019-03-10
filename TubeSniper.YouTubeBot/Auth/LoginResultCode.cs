namespace TubeSniper.YouTubeBot.Auth
{
    public enum LoginResultCode
    {
        Success,
        HttpError,
        BadCredentials,
        BadRecoveryCredentials,
		AccountSuspended,
		AccountNotFound,
        CaptchaSolveFail,
        Failure,
	    ProxyError
    }
}