namespace TubeSniper.Core.Domain.Auth
{
	public interface IAuthService
	{
		/*License VerifyLicense(LicenseKey licenseKey);*/
		LicenseKey GetStoredKey();
		CheckNewActivationCode CheckNewActivation();
		MyIsGenuineResult CheckActivation();
		bool SaveLicenseKey(LicenseKey licenseKey);
		bool IsStoredKeyValid();
		IsActivatedResult IsActivated();
		SelectorPayload GetSelectorPayload(string key);
	}
}