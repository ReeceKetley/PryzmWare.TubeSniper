using System;
using System.Windows.Input;
using TubeSniper.Application.Campaigns;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.Views.CampaignEditor;
using TubeSniper.Presentation.Wpf.Views.Campaigns;

namespace TubeSniper.Presentation.Wpf.ViewModels.Campaigns
{
	public class CampaignTileViewModel : ViewModelBase
	{
		private readonly ICampaignService _campaignService;
		private ICommand _advancedCommand;
		private ICommand _deleteCommand;
		private ICommand _editCommand;
		private ICommand _startCommand;
		private ICommand _stopCommand;
		private ThreadedCampaign _threadedCampaign;
		private Campaign _updatedCampaign;

		public CampaignTileViewModel(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		public ICommand StartCommand => _startCommand ?? (_startCommand = new RelayCommand(Start));
		public ICommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(Edit));
		public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));
		public ICommand AdvancedCommand => _advancedCommand ?? (_advancedCommand = new RelayCommand(AdvancedView));

		public bool IsRunning { get; private set; }
		public string Title { get; private set; }
		public int SuccessCount { get; set; }
		public int ErrorCount { get; set; }
		public string StartButtonText { get; private set; } = "Start Campaign";

		public Campaign Campaign { get; private set; }

		public event EventHandler<EventArgs> CommentPosted;

		public event EventHandler<EventArgs> ErrorFired;

		public void SetCampaign(Campaign campaign)
		{
			if (Campaign != null)
			{
				if (Campaign.IsRunning)
				{
					_updatedCampaign = campaign;
					return;
				}

				UnsetCampaign();
			}

			Campaign = campaign;
			Title = campaign.Title.Value;
			Campaign.SuccessCountChanged += Campaign_SuccessCountChanged;
			Campaign.ErrorCountChanged += Campaign_ErrorCountChanged;
			Campaign.Stopped += Campaign_OnStopped;
			_threadedCampaign = new ThreadedCampaign(campaign);
		}

		private void UnsetCampaign()
		{
			_updatedCampaign = null;
			Campaign.SuccessCountChanged -= Campaign_SuccessCountChanged;
			Campaign.ErrorCountChanged -= Campaign_ErrorCountChanged;
			Campaign.Stopped -= Campaign_OnStopped;
		}

		private void Start(object obj)
		{
			Console.WriteLine(IsRunning);
			if (Campaign.IsRunning)
			{
				StartButtonText = "Start Campaign";
				Campaign.Stop();
			}
			else
			{
				StartButtonText = "Stop Campaign";
				_threadedCampaign.Start();
			}
		}

		private void Delete(object obj)
		{
			if (Campaign.IsRunning)
			{
				return;
			}

			_campaignService.Remove(Campaign);
		}

		private void Edit(object obj)
		{
			if (Campaign.IsRunning)
			{
				return;
			}

			var viewModel = ViewModelFactory.Campaigns.CampaignEditorViewModel();
			viewModel.SetCampaign(Campaign);
			var edit = new CampaignEditorView(viewModel);
			edit.Show();
		}

		private void AdvancedView(object obj)
		{
			AdvancedCampaignView view = new AdvancedCampaignView();
			view.Show();
			var viewModel = (CampaignAdvancedViewModel) view.DataContext;
			viewModel.Campaign = Campaign;
			viewModel.Setup();
		}

		private void Campaign_OnStopped(object sender, EventArgs e)
		{
			if (_updatedCampaign == null)
			{
				return;
			}

			SetCampaign(_updatedCampaign);
		}

		private void Campaign_SuccessCountChanged(object sender, EventArgs e)
		{
			SuccessCount = Campaign.SuccessCount;
		}

		private void Campaign_ErrorCountChanged(object sender, EventArgs e)
		{
			ErrorCount = Campaign.ErrorCount;
			OnErrorFired();
		}

		protected virtual void OnCommentPosted()
		{
			CommentPosted?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnErrorFired()
		{
			ErrorFired?.Invoke(this, EventArgs.Empty);
		}
	}
}