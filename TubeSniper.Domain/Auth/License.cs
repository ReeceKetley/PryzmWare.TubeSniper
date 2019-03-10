namespace TubeSniper.Domain.Auth
{
	public class License
	{
		public bool IsActivated;
		public bool IsGenuine;
		public ProductKey ProductKey;

		public License(bool isGenuine, bool isActivated, ProductKey productKey)
		{
			IsGenuine = isGenuine;
			IsActivated = isActivated;
			ProductKey = productKey;
		}
	}
}