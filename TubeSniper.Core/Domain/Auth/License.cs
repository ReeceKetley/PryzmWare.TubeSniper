namespace TubeSniper.Core.Domain.Auth
{
	public class License
	{
		public bool IsGenuine;
		public bool IsActivated;
		public ProductKey ProductKey;

		public License(bool isGenuine, bool isActivated, ProductKey productKey)
		{
			IsGenuine = isGenuine;
			IsActivated = isActivated;
			ProductKey = productKey;
		}
	}
}