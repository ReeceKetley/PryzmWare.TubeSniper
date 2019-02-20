using System;
using System.Windows;
using System.Windows.Forms;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.ViewModels.Auth;
using Application = System.Windows.Application;
using AuthLoadingView = TubeSniper.Presentation.Wpf.Views.Auth.AuthLoadingView;
using AuthView = TubeSniper.Presentation.Wpf.Views.Auth.AuthView;

namespace TubeSniper.Presentation.Wpf.Openers
{
	public class MainWindowOpener
	{
		private readonly IAuthService _authService;
		private Application _app;

		public MainWindowOpener(IAuthService authService)
		{
			_authService = authService;
		}

		public void StartApplication()
		{
			var app = new Application();
			app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			_app = app;
			LicenseKey licenseKey;
			License result = null;

			if (!LicenseCheck())
			{
				return;
			}

			new Views.MainWindow.MainWindowView().Show();
			app.Run();
		}

		private bool LicenseCheck()
		{
			for (; ; )
			{
				switch (NewActivation())
				{
					case NewActivationCode.True:
						return true;
					case NewActivationCode.False:
						return false;
					case NewActivationCode.Continue:
						continue;
					case NewActivationCode.Next:
						break;
					default:
						return false;
				}

				var gr = CheckActivation();
				if (gr == MyIsGenuineResult.InternetError)
				{
					System.Windows.Forms.MessageBox.Show("Unable to reach activation servers. Please check your internet connection and try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return false;
				}

				if (gr == MyIsGenuineResult.Genuine || gr == MyIsGenuineResult.GenuineFeaturesChanged)
				{
					return true;
				}
			}
		}

		private NewActivationCode NewActivation()
		{
			var isKeyValid = _authService.IsStoredKeyValid();
			if (isKeyValid)
			{
				var isActivatedResult = _authService.IsActivated();
				switch (isActivatedResult)
				{
					case IsActivatedResult.IsActivated:
						return NewActivationCode.Next;
					case IsActivatedResult.EnableNetworkAdaptersException:
						System.Windows.Forms.MessageBox.Show("You must enable your network adpaters", "TubeSniper", MessageBoxButtons.OK);
						return NewActivationCode.False;
					case IsActivatedResult.TurboActivateException:
						System.Windows.Forms.MessageBox.Show("Activation Exception", "TubeSniper", MessageBoxButtons.OK);
						return NewActivationCode.False;
					case IsActivatedResult.NotActivated:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			var licenseKey = PromptLicenseKey();
			if (licenseKey == null)
			{
				return NewActivationCode.False;
			}

			if (!_authService.SaveLicenseKey(licenseKey))
			{
				System.Windows.Forms.MessageBox.Show("You must enter a valid product key before activating online. Check your product key, and type it again.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			if (!CheckNewActivation())
			{
				return NewActivationCode.Continue;
			}

			System.Windows.Forms.MessageBox.Show("You have now activated this computer.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			return NewActivationCode.True;
		}

		private LicenseKey PromptLicenseKey()
		{
			var viewModel = ViewModelFactory.AuthViewModel();
			var view = new AuthView(viewModel);
			view.ShowDialog();
			//_app.Run(view);
			if (viewModel.Key == null)
			{
				return null;
			}

			return viewModel.Key;
		}

		private MyIsGenuineResult CheckActivation()
		{
			var viewModel = ViewModelLocator.Locate<AuthLoadingViewModel>();
			var view = new AuthLoadingView(viewModel, false);
			view.ShowDialog();
			//_app.Run(view);
			var result = viewModel.CheckActivationResult;
			//System.Windows.Forms.MessageBox.Show("Code: " + result.ToString(), "TubeSniper");
			switch (result)
			{
				case MyIsGenuineResult.Genuine:
					break;
				case MyIsGenuineResult.GenuineFeaturesChanged:
					break;
				case MyIsGenuineResult.NotGenuine:
					break;
				case MyIsGenuineResult.NotGenuineInVM:
					break;
				case MyIsGenuineResult.InternetError:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return result;
			//return viewModel.IsGenuineResult;
		}

		private bool CheckNewActivation()
		{
			var viewModel = ViewModelLocator.Locate<AuthLoadingViewModel>();
			var view = new AuthLoadingView(viewModel, true);
			view.ShowDialog();
			var result = viewModel.CheckNewActivationResult;
			//System.Windows.Forms.MessageBox.Show("Code: " + result.ToString(), "TubeSniper");
			switch (result)
			{
				case CheckNewActivationCode.InvalidProductKeyException:
					break;
				case CheckNewActivationCode.InternetException:
					break;
				case CheckNewActivationCode.PkeyMaxUsedException:
					break;
				case CheckNewActivationCode.PkeyRevokedException:
					break;
				case CheckNewActivationCode.EnableNetworkAdaptersException:
					break;
				case CheckNewActivationCode.DateTimeException:
					break;
				case CheckNewActivationCode.VirtualMachineException:
					break;
				case CheckNewActivationCode.Exception:
					break;
				case CheckNewActivationCode.IsActivated:
					System.Windows.Forms.MessageBox.Show("Product Activated.");
					return true;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return false;
		}

		private enum NewActivationCode
		{
			True,
			False,
			Continue,
			Next
		}
	}
}

