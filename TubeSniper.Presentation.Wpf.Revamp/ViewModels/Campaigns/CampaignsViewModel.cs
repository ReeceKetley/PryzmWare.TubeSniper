using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TubeSniper.Core.Application.Events.Campaigns;
using TubeSniper.Core.Interfaces;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.Views.CampaignEditor;

namespace TubeSniper.Presentation.Wpf.ViewModels.Campaigns
{
	public class CampaignsViewModel : ViewModelBase
	{
		private readonly ICampaignService _campaignService;
		private ICommand _createCampaign;

		public CampaignsViewModel(ICampaignService campaignService)
		{
			_campaignService = campaignService;
			Tiles = new ObservableCollection<CampaignTileViewModel>();
			foreach (var campaign in campaignService.GetAll())
			{
				var viewModel = ViewModelFactory.Campaigns.CampaignTileViewModel();
				viewModel.Campaign = campaign;
				Tiles.Add(viewModel);
			}

			CampaignEvents.CampaignCreated += CampaignEvents_OnCampaignCreated;
			CampaignEvents.CampaignRemoved += CampaignEvents_OnCampaignRemoved;
			CampaignEvents.CampaignUpdated += CampaignEvents_OnCampaignUpdated;
		}

		public ICommand CreateCampaignCommand => _createCampaign ?? (_createCampaign = new RelayCommand(CreateCampaign));

		public ObservableCollection<CampaignTileViewModel> Tiles { get; set; }

		private void CreateCampaign(object obj)
		{
			var viewModel = ViewModelFactory.Campaigns.CampaignEditorViewModel();
			var view = new CampaignEditorView(viewModel);
			view.ShowDialog();
		}

		private void CampaignEvents_OnCampaignCreated(object sender, CampaignCreated e)
		{
			var viewModel = ViewModelFactory.Campaigns.CampaignTileViewModel();
			viewModel.Campaign = e.Campaign;
			Tiles.Add(viewModel);
		}

		private void CampaignEvents_OnCampaignRemoved(object sender, CampaignRemoved e)
		{
			var viewModels = Tiles.Where(x => x.Campaign == e.Campaign).ToList();
			foreach (var viewModel in viewModels)
			{
				Tiles.Remove(viewModel);
			}
		}

		private void CampaignEvents_OnCampaignUpdated(object sender, CampaignUpdated e)
		{
			// do something here
		}
	}
}