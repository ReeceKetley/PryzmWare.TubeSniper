using System.Windows.Controls;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.ViewModels.SettingsEditor;

namespace TubeSniper.Presentation.Wpf.Views.SettingsEditor
{
    /// <summary>
    /// Interaction logic for LicenseView.xaml
    /// </summary>
    public partial class LicenseView : UserControl
    {
        public LicenseView()
        {
            InitializeComponent();
	        DataContext = ViewModelLocator.Locate<LicenseViewModel>();
		}
    }
}
