using System;
using TubeSniper.Domain.Auth;
using TubeSniper.Infrastructure.Common;

namespace TubeSniper.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
		public byte[] GetYoutubeBotData(string key)
		{
			return null;
			/*
			GetAuthPayloadResponse response = null;
			var apiClient = new TubeSniperApiClient();
			try
			{
				response = apiClient.PostAuthPayload(new GetAuthPayloadRequest() {LicenseKey = key});
			}
			catch (Exception)
			{
				return null;
			}

			if (response != null && response.Response["CloudRailKey"] == null)
			{
				return null  ;
			}
			SelectorPayload dto = null;
			try
			{
				if (response != null) dto = JsonConvert.DeserializeObject<SelectorPayload>(response.Response.ToString(), new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
			}
			catch (JsonException)
			{
				throw new Exception();
			}

			return dto;
			*/
		}

		public void DeactiveKey()
		{
			var turboActivate = GetTurboActivate();
			turboActivate.Deactivate(true);
		}

		public ProductKey GetStoredKey()
		{
			var turboActivate = GetTurboActivate();
			return new ProductKey(turboActivate.GetPKey());
		}

		public CheckNewActivationCode CheckNewActivation()
		{
			var turboActivate = GetTurboActivate();
			try
			{
				turboActivate.Activate();
				var result = turboActivate.IsGenuine();
				if (result != IsGenuineResult.Genuine && result != IsGenuineResult.GenuineFeaturesChanged)
				{
					if (result == IsGenuineResult.InternetError)
					{
						throw new InternetException();
					}
					throw new InvalidProductKeyException();
				}
				//isActivated = true;
			}
			catch (InvalidProductKeyException)
			{
				return CheckNewActivationCode.InvalidProductKeyException;
				//System.Windows.Forms.MessageBox.Show("The product key you typed is invalid. If you believe this is an error, please contact support.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (InternetException)
			{
				return CheckNewActivationCode.InternetException;
				//System.Windows.Forms.MessageBox.Show("Unable to reach activation servers. Please check your internet connection and try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (PkeyMaxUsedException)
			{
				return CheckNewActivationCode.PkeyMaxUsedException;
				//System.Windows.Forms.MessageBox.Show("This product key already appears to have been activated with the maximum number of computers.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (PkeyRevokedException)
			{
				return CheckNewActivationCode.PkeyRevokedException;
				//System.Windows.Forms.MessageBox.Show("This product key appears to have been revoked. If you believe this is an error, please open a support ticket.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (EnableNetworkAdaptersException)
			{
				return CheckNewActivationCode.EnableNetworkAdaptersException;
				//System.Windows.Forms.MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (DateTimeException)
			{
				return CheckNewActivationCode.DateTimeException;
				//System.Windows.Forms.MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (VirtualMachineException)
			{
				return CheckNewActivationCode.VirtualMachineException;
				//System.Windows.Forms.MessageBox.Show("This license is not permitted on virtual machines.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception)
			{
				return CheckNewActivationCode.Exception;
				//System.Windows.Forms.MessageBox.Show("An unhandled exception has occurred. Please contact support for help on resolving this issue. (Quote this information: " + e.Message + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return CheckNewActivationCode.IsActivated;
			//return isActivated;
		}

		public MyIsGenuineResult CheckActivation()
		{
			var turboActivate = GetTurboActivate();
			IsGenuineResult gr;
			try
			{
				gr = turboActivate.IsGenuine();
			}
			catch (DateTimeException)
			{
				//gr MyIsGenuineResult.NotGenuine;
				gr = IsGenuineResult.NotGenuine;
				//System.Windows.Forms.MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception)
			{
				/*
				if (form != null)
				{
					form.Invoke(new Action(form.Hide));
				}
				*/
				//System.Windows.Forms.MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				//gr = IsGenuineResult.NotGenuine;
				return MyIsGenuineResult.NotGenuine;
			}

			switch (gr)
			{
				case IsGenuineResult.Genuine:
					return MyIsGenuineResult.Genuine;
				case IsGenuineResult.GenuineFeaturesChanged:
					return MyIsGenuineResult.GenuineFeaturesChanged;
				case IsGenuineResult.NotGenuine:
					return MyIsGenuineResult.NotGenuine;
				case IsGenuineResult.NotGenuineInVM:
					return MyIsGenuineResult.NotGenuineInVM;
				case IsGenuineResult.InternetError:
					return MyIsGenuineResult.InternetError;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public bool SaveLicenseKey(ProductKey productKey)
		{
			var turboActivate = GetTurboActivate();
			if (!turboActivate.CheckAndSavePKey(productKey.Value))
			{
				return false;

				//return NewActivationCode.Continue;
			}

			return true;
		}

		public bool IsStoredKeyValid()
		{
			var turboActivate = GetTurboActivate();
			return turboActivate.IsProductKeyValid();
		}

		public IsActivatedResult IsActivated()
		{
			var turboActivate = GetTurboActivate();
			try
			{
				if (turboActivate.IsActivated())
				{
					return IsActivatedResult.IsActivated;
					//return NewActivationCode.Next;
				}
			}
			catch (EnableNetworkAdaptersException)
			{
				return IsActivatedResult.EnableNetworkAdaptersException;
				// network down
				//return NewActivationCode.False;
			}
			catch (TurboActivateException)
			{
				return IsActivatedResult.TurboActivateException;
				//return NewActivationCode.False;
			}

			return IsActivatedResult.NotActivated;
		}

		private TurboActivate GetTurboActivate()
		{
			return new TurboActivate("p7uihgr7ty6kb5avb6aexqhxzkktvay");
		}
	}
}
