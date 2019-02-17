using System.Windows.Controls;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.Views.SuccessMonitor
{
    /// <summary>
    /// Interaction logic for SuccessView.xaml
    /// </summary>
    public partial class SuccessView : UserControl
    {
        public SuccessView()
        {
            InitializeComponent();
	        if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
	        {
		        return;
	        }


	        DataContext = ViewModelFactory.Campaigns.SuccessMoniterViewModel();
		}
    }
}
