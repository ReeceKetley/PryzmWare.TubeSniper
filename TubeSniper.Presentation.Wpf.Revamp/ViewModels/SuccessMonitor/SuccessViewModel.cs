using System.Collections.ObjectModel;
using System.Windows.Data;
using TubeSniper.Core;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Presentation.Wpf.Common;

namespace TubeSniper.Presentation.Wpf.ViewModels.SuccessMonitor
{
	public class SuccessMoniterViewModel : ViewModelBase
	{

		private static object _lock = new object();
		public SuccessMoniterViewModel()
		{
			Tiles = new ObservableCollection<SuccessViewTileViewModel>();
			BindingOperations.EnableCollectionSynchronization(Tiles, _lock);

			SharedData.CampaignProcessed += SharedData_CampaignProcessed;
		}

		private void SharedData_CampaignProcessed(object sender, CampaignProcessedEventArgs e)
		{
			var viewModel = ViewModelFactory.Campaigns.SuccessViewTileViewModel();
			viewModel.CampaignTitle = e.CampaignMeta.Title.Trim();
			viewModel.Url = "https://www.youtube.com/watch?v=" + e.Meta.GetId();
			viewModel.Comment = e.Comment.Trim();
			viewModel.VideoTitle = e.Meta.GetTitle().Trim();
			viewModel.ImageSource = e.Meta.GetThumbnailUrl();
			viewModel.SearchTerm = e.CampaignMeta.SearchTerm.Trim();
			Tiles.Add(viewModel);
		}

		public ObservableCollection<SuccessViewTileViewModel> Tiles { get; set; }
	}
}