using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Threading;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Presentation.Wpf.ViewModels.Campaigns
{
	public class CampaignAdvancedViewModel : ViewModelBase
	{
		private object _statusLock = new object();

		public CampaignAdvancedViewModel()
		{
			Setup();
		}

		public Campaign Campaign { get; set; }
		public ObservableCollection<string> CredentailList { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> StatusLog { get; set; } = new ObservableCollection<string>();

		private void Campaign_StatusChanged(object sender, StatusChangedEventArgs e)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => { StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Status Event] - " + e.Status); }));
		}

		public void Setup()
		{
			BindingOperations.EnableCollectionSynchronization(StatusLog, _statusLock);
			if (Campaign?.StatusLog == null)
			{
				StatusLog = new ObservableCollection<string>();
			}

			if (Campaign != null)
			{
				Campaign.StatusChanged += Campaign_StatusChanged;
				Campaign.VideoProcessed += CampaignOnVideoProcessed;
				Campaign.FatalError += CampaignOnFatalError;
				foreach (var campaignMetaAccount in Campaign.Meta.Accounts)
				{
					//Console.WriteLine(campaignMetaAccount.Credentials.Email);
					CredentailList.Add(campaignMetaAccount.Credentials.Email);
				}
			}
		}

		private void CampaignOnNetworkError(object sender, EventArgs e)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => { StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Unkown Network Error] - Retrying"); }));
		}

		private void CampaignOnFatalError(object sender, FatalErrorEventArgs e)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => { StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Error Event] - " + e.Error); }));
		}

		private void CampaignOnVideoProcessed(object sender, VideoProcessedEventArgs e)
		{
			System.Windows.Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => { StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Video Processed] - " + e.Video.Title + " - " + e.Comment + " - " + e.Video.Url); }));
		}
	}
}