﻿namespace TubeSniper.Core.Domain.Auth
{
	public interface IAuthService
	{
		/*License VerifyLicense(LicenseKey licenseKey);*/
		ProductKey GetStoredKey();
		CheckNewActivationCode CheckNewActivation();
		MyIsGenuineResult CheckActivation();
		bool SaveLicenseKey(ProductKey productKey);
		bool IsStoredKeyValid();
		IsActivatedResult IsActivated();
		SelectorPayload GetSelectorPayload(string key);
		void DeactiveKey();
	}
}