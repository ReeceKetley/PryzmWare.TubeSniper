using System.Collections.ObjectModel;
using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Presentation.Wpf.ViewModels.Campaigns
{
	public class CampaignAdvancedViewModel : ViewModelBase
	{
		private readonly object _statusLock = new object();

		public CampaignAdvancedViewModel()
		{
			Setup();
		}

		public Campaign Campaign { get; set; }
		public ObservableCollection<string> CredentialList { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> StatusLog { get; set; } = new ObservableCollection<string>();

		public void Setup()
		{
			if (Campaign != null)
			{
//				foreach (var campaignMetaAccount in Campaign.Meta.Accounts)
//				{
//					CredentialList.Add(campaignMetaAccount.Credentials.Email);
//				}
			}
		}

	}
}