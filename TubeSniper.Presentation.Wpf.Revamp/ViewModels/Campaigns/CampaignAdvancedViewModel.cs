using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Presentation.Wpf.ViewModels.Campaigns
{
	public class CampaignAdvancedViewModel : ViewModelBase
	{
		private object _statusLock = new object();
		public Campaign Campaign { get; set; }
		public ObservableCollection<string> CredentailList { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> StatusLog { get; set; } = new ObservableCollection<string>();

		public CampaignAdvancedViewModel()
		{
			Setup();
		}

		private void Campaign_StatusChanged(object sender, Core.Domain.Youtube.StatusChangedEventArgs e)
		{
			Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => {
					StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Status Event] - " + e.Status);
				}));
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
				Campaign.NetworkError += CampaignOnNetworkError;
				foreach (var campaignMetaAccount in Campaign.CampaignMeta.Accounts)
				{
					//Console.WriteLine(campaignMetaAccount.Credentials.Email);
					CredentailList.Add(campaignMetaAccount.Credentials.Email);
				}
			}
		}

		private void CampaignOnNetworkError(object sender, EventArgs eventArgs)
		{
			Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => {
					StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Unkown Network Error] - Retrying");
				}));
		}

		private void CampaignOnFatalError(object sender, FatalErrorEventArgs fatalErrorEventArgs)
		{
			Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => {
					StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Error Event] - " + fatalErrorEventArgs.Error);
				}));
		}

		private void CampaignOnVideoProcessed(object sender, VideoProcessedEventArgs videoProcessedEventArgs)
		{
			Application.Current.Dispatcher.BeginInvoke(
				DispatcherPriority.Background,
				new Action(() => {
					StatusLog.Add("[" + DateTime.Now.ToShortTimeString() + "] [Video Processed] - " + videoProcessedEventArgs.Meta.GetTitle() + " - " + videoProcessedEventArgs.Comment + " - https://youtube.com/watch?v=" + videoProcessedEventArgs.Meta.GetId());
				}));
		}
	}
}