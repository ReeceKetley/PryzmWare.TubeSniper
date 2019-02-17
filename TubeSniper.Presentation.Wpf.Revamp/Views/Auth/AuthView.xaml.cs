using System;
using System.Windows;
using TubeSniper.Presentation.Wpf.ViewModels.Auth;

namespace TubeSniper.Presentation.Wpf.Views.Auth
{
	/// <summary>
	/// Interaction logic for AuthView.xaml
	/// </summary>
	public partial class AuthView : Window
	{
		private readonly AuthViewModel _viewModel;

		public AuthView()
		{
			InitializeComponent();
		}

		public AuthView(AuthViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;
			_viewModel.Submitted += Window_OnSubmitted;
		}

		private void Window_OnSubmitted(object sender, EventArgs eventArgs)
		{
			Close();
		}


		private void Window_OnClosed(object sender, EventArgs e)
		{
			_viewModel.Submitted -= Window_OnSubmitted;
		}
	}
}
