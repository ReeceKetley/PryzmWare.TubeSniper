using System.Windows.Controls;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.Views.Campaigns
{
	/// <summary>
	/// Interaction logic for MainPageView.xaml
	/// </summary>
	public partial class CampaignsView : UserControl
	{
		public CampaignsView()
		{
			InitializeComponent();
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}
			

			DataContext = ViewModelFactory.Campaigns.CampaignsViewModel();
		}
	}
}
