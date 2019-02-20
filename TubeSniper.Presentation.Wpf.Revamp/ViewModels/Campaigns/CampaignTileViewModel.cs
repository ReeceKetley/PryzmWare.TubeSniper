using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using TubeSniper.Core;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces;
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
		private int _proccesCount;
		private ICommand _startCommand;
		private object _statusLock = new object();
		private ICommand _stopCommand;
		private int _warningCount;


		public CampaignTileViewModel(ICampaignService campaignService)
		{
			_campaignService = campaignService;
			_warningCount = 0;
			BindingOperations.EnableCollectionSynchronization(StatusLog, _statusLock);
			SharedData.CurrentStep += SharedData_CurrentStep;
		}

		public ObservableCollection<string> StatusLog { get; private set; } = new ObservableCollection<string>();
		public string SuccesCount { get; set; }
		public string WarningCount { get; set; }
		public bool IsRunning { get; private set; }
		public string Title { get; private set; }
		public string StartButtonText { get; private set; } = "Start Campaign";
		public string CurerntImage { get; private set; }

		public ICommand StartCommand => _startCommand ?? (_startCommand = new RelayCommand(Start));


		public ICommand StopCommand => _stopCommand ?? (_stopCommand = new RelayCommand(Stop));
		public ICommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(Edit));
		public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));
		public ICommand AdvancedCommand => _advancedCommand ?? (_advancedCommand = new RelayCommand(AdvancedView));

		public string State { get; set; }
		public bool Editable { get; set; }
		public string Status { get; set; }

		public Campaign Campaign { get; set; }
		public event EventHandler<EventArgs> WarningFired;
		public event EventHandler<EventArgs> VideoProcessed;
		public event EventHandler<EventArgs> ErrorFired;

		private void SharedData_CurrentStep(object sender, CurrentStepEventArgs e)
		{
			Console.WriteLine(e.ToString());
			switch (e)
			{
				case CurrentStepEventArgs.Searching:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Search More_32px.png";
					break;
				case CurrentStepEventArgs.EstablishingProxyConnection:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Computers Connecting_32px.png";
					break;
				case CurrentStepEventArgs.CommentPosted:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Ok_32px.png";
					break;
				case CurrentStepEventArgs.Commenting:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Comments_32px.png";
					break;
				case CurrentStepEventArgs.Downloading:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Downloading Updates_32px.png";
					break;
				case CurrentStepEventArgs.EstablishingProxyConnectionFailled:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Error Cloud_32px.png";
					break;
				case CurrentStepEventArgs.Failure:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Error_32px.png";
					break;
				case CurrentStepEventArgs.LoggedIn:
					CurerntImage = "/TubeSniper.Presentation.Wpf;component/Resources/Checked User Male_32px.png";
					break;
			}
		}

		private void Campaign_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			Status = e.Status;
			StatusLog.Add(e.Status);
		}

		private void Campaign_VideoProcessed(object sender, VideoProcessedEventArgs e)
		{
			Status = e.Video.Title + " - Video Proccesed";
			++_proccesCount;
			SuccesCount = _proccesCount > 99 ? "99+" : _proccesCount.ToString();
			OnVideoProcessed();
		}

		private void Campaign_FatalError(object sender, FatalErrorEventArgs e)
		{
			Status = "Fatal Error: " + e.Error;
			++_warningCount;
			WarningCount = _warningCount > 99 ? "99+" : _warningCount.ToString();
			OnWarningFired();
		}

		private void AdvancedView(object obj)
		{
			AdvancedCampaignView view = new AdvancedCampaignView();
			view.Show();
			var viewModel = (CampaignAdvancedViewModel)view.DataContext;
			viewModel.Campaign = Campaign;
			viewModel.Setup();
		}

		private void Delete(object obj)
		{
			if (IsRunning)
			{
				Campaign.Stop();
			}

			_campaignService.Remove(Campaign);
		}

		private void InstallEvents()
		{
			Campaign.StatusChanged += Campaign_StatusChanged;
			Campaign.VideoProcessed += Campaign_VideoProcessed;
			Campaign.FatalError += Campaign_FatalError;
			Campaign.NetworkError += Campaign_NetworkError;
			Campaign.CampaignStarted += Campaign_CampaignStarted;
			Campaign.CampaignStopped += Campaign_CampaignStopped;
		}

		private void Campaign_NetworkError(object sender, EventArgs e)
		{
			++_warningCount;
			WarningCount = _warningCount > 99 ? "99+" : _warningCount.ToString();
		}

		private void Campaign_CampaignStopped(object sender, EventArgs e)
		{
			IsRunning = false;
		}

		private void Campaign_CampaignStarted(object sender, EventArgs e)
		{
			IsRunning = true;
		}

		private void Start(object obj)
		{
			Console.WriteLine(IsRunning);
			if (IsRunning)
			{
				StartButtonText = "Start Campaign";
				Campaign.Stop();
			}
			else
			{
				StartButtonText = "Stop Campaign";
				Campaign.Start();
			}
		}

		private void Stop(object obj)
		{
		}

		private void Edit(object obj)
		{
			if (IsRunning)
			{
				Campaign.Stop();
			}

			var viewModel = ViewModelFactory.Campaigns.CampaignEditorViewModel();
			viewModel.SetCampaign(Campaign);
			var edit = new CampaignEditorView(viewModel);
			edit.Show();
		}

		// ReSharper disable once UnusedMember.Local
		private void OnCampaignChanged()
		{
			Title = Campaign.CampaignMeta.Title;
			InstallEvents();
		}

		protected virtual void OnWarningFired()
		{
			WarningFired?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnVideoProcessed()
		{
			VideoProcessed?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnErrorFired()
		{
			ErrorFired?.Invoke(this, EventArgs.Empty);
		}
	}
}