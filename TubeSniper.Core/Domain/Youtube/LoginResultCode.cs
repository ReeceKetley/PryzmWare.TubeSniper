namespace TubeSniper.Core.Domain.Youtube
{
    public enum LoginResultCode
    {
        Success,
        HttpError,
        BadCredentials,
        ObjectNotFound,
        BadRecoveryCredentials,
		AccountSuspended,
		AccountNotFound,
        CaptchaSolveFail,
        SetRecovery,
        Failure,
        Timeout,
	    ProxyError
    }
}