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
		}
	}
}
