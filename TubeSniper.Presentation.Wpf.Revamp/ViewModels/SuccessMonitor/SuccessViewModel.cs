using System.Collections.ObjectModel;
using System.Windows.Data;
using TubeSniper.Core;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.ViewModels.SuccessMonitor
{
	public class SuccessMonitorViewModel : ViewModelBase
	{
		private static readonly object _lock = new object();

		public SuccessMonitorViewModel()
		{
			Tiles = new ObservableCollection<SuccessViewTileViewModel>();
			BindingOperations.EnableCollectionSynchronization(Tiles, _lock);

			SharedData.CampaignProcessed += SharedData_CampaignProcessed;
		}

		public ObservableCollection<SuccessViewTileViewModel> Tiles { get; set; }

		private void SharedData_CampaignProcessed(object sender, CampaignProcessedEventArgs e)
		{
			var viewModel = ViewModelFactory.Campaigns.SuccessViewTileViewModel();
			viewModel.SetVideo(e.Video, e.CampaignMeta, e.Comment);
			Tiles.Add(viewModel);
		}
	}
}