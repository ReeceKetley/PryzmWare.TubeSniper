using System.Windows;
using TubeSniper.Presentation.Wpf.Accounts.ViewModels;

namespace TubeSniper.Presentation.Wpf.Accounts.Views
{
	/// <summary>
	/// Interaction logic for CreateProxyView.xaml
	/// </summary>
	public partial class AccountEditorView : Window
	{
		public AccountEditorView(AccountEditorViewModel viewModel)
		{
			InitializeComponent();
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}
			DataContext = viewModel;

			if (viewModel.CloseAction == null)
			{
				viewModel.CloseAction = Close;
			}
		}
	}
}
