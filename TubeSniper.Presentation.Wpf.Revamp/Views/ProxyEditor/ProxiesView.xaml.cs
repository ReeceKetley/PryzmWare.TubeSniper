using System.Windows.Controls;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.Views.ProxyEditor
{
    /// <summary>
    /// Interaction logic for ProxiesPageView.xaml
    /// </summary>
    public partial class ProxiesView : UserControl
    {
        public ProxiesView()
        {
            InitializeComponent();
	        DataContext = ViewModelFactory.Proxies.ProxiesViewModel();
        }

    }
}
