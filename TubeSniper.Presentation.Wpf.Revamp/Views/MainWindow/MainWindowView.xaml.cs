using System.Security.Principal;
using System.Windows;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.Views.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
	        InitializeComponent();
	        if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
	        {
		        return;
	        }
	        DataContext = ViewModelFactory.MainWindowViewModel();
			SomeMethod();
		}

	    public bool IsElevated
	    {
		    get
		    {
			    return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
		    }
	    }

	    public void SomeMethod()
	    {
		    if (!this.IsElevated)
		    {
			    System.Windows.MessageBox.Show("TubeSniper has detected it is not running as an Administrator. You may experience some instabilities. Recommend closing and starting as Administrator.", "Polite Notice", MessageBoxButton.OK);
		    }

		    this.Width = 900;
	    }
	}
}
