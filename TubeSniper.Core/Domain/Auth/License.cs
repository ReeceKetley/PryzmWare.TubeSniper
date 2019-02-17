namespace TubeSniper.Core.Domain.Auth
{
	public class License
	{
		public bool IsGenuine;
		public bool IsActivated;
		public LicenseKey LicenseKey;

		public License(bool isGenuine, bool isActivated, LicenseKey licenseKey)
		{
			IsGenuine = isGenuine;
			IsActivated = isActivated;
			LicenseKey = licenseKey;
		}
	}
}