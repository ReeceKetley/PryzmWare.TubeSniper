namespace TubeSniper.Application
{
	public enum CommentServiceResultArgs
	{
		Success,
		HttpError,
		BadCredentials,
		BadRecoveryCredentials,
		AccountSuspended,
		AccountNotFound,
		Failure,
		ProxyError
	}
}