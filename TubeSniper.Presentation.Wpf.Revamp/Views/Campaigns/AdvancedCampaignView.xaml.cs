using MahApps.Metro.Controls;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.Views.Campaigns
{
    /// <summary>
    /// Interaction logic for AdvancedCampaignView.xaml
    /// </summary>
    public partial class AdvancedCampaignView : MetroWindow
    {
        public AdvancedCampaignView()
        {
            InitializeComponent();
	        DataContext = ViewModelFactory.Campaigns.CampaignAdvancedViewModel();
        }
    }
}
