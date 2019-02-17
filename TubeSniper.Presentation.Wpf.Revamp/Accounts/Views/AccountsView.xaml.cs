using System.Windows.Controls;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.Accounts.Views
{
	/// <summary>
	/// Interaction logic for AccountsViewPage.xaml
	/// </summary>
	public partial class AccountsView : UserControl
	{
		public AccountsView()
		{
			InitializeComponent();
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}

			DataContext = ViewModelFactory.Accounts.AccountsViewModel();
		}
	}
}
