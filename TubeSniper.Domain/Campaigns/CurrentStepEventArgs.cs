namespace TubeSniper.Domain.Campaigns
{
	public enum CurrentStepEventArgs
	{
		Searching,
		Downloading,
		Commenting,
		LogingIn,
		EstablishingProxyConnection,
		Failure,
		CommentPosted,
		EstablishingProxyConnectionFailled,
		ProxyConectionEstablished,
		LoggedIn
	}
}