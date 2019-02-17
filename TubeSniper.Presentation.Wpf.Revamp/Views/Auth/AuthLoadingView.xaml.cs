using System;
using System.Windows;
using MahApps.Metro.Controls;
using TubeSniper.Presentation.Wpf.ViewModels.Auth;

namespace TubeSniper.Presentation.Wpf.Views.Auth
{
	/// <summary>
	/// Interaction logic for AuthLoadingView.xaml
	/// </summary>
	public partial class AuthLoadingView : Window
	{
		private readonly AuthLoadingViewModel _viewModel;

		public AuthLoadingView()
		{
			InitializeComponent();
		}

		public AuthLoadingView(AuthLoadingViewModel viewModel, bool hacky)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;
			viewModel.CheckNewActivationCompleted += ViewModel_CheckNewActivationCompleted;
			if (hacky)
			{
				_viewModel.CheckNewActivation();
			}
			else
			{
				_viewModel.CheckActivation();
			}
		}

		private void ViewModel_CheckNewActivationCompleted(object sender, System.EventArgs e)
		{
			this.Invoke(new Action( delegate { this.Close(); }));
		}

	}
}