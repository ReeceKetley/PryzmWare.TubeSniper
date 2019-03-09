namespace TubeSniper.Domain.Youtube
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