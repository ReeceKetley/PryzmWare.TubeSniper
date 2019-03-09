using TubeSniper.Domain.Common;
using TubeSniper.Presentation.Wpf.Accounts.ViewModels;
using TubeSniper.Presentation.Wpf.ViewModels.Auth;
using TubeSniper.Presentation.Wpf.ViewModels.CampaignEditor;
using TubeSniper.Presentation.Wpf.ViewModels.Campaigns;
using TubeSniper.Presentation.Wpf.ViewModels.MainWindow;
using TubeSniper.Presentation.Wpf.ViewModels.ProxyEditor;
using TubeSniper.Presentation.Wpf.ViewModels.SuccessMonitor;
using CampaignsViewModel = TubeSniper.Presentation.Wpf.ViewModels.Campaigns.CampaignsViewModel;

namespace TubeSniper.Presentation.Wpf.Common
{
	public static class ViewModelFactory
	{
		public static MainWindowViewModel MainWindowViewModel()
		{
			return GlobalContainer.Container.Resolve<MainWindowViewModel>();
		}
		public static AuthViewModel AuthViewModel()
		{
			return GlobalContainer.Container.Resolve<AuthViewModel>();
		}

		public static class Accounts
		{
			public static AccountsViewModel AccountsViewModel()
			{
				return GlobalContainer.Container.Resolve<AccountsViewModel>();
			}
		}

		public static class Campaigns
		{
			public static CampaignsViewModel CampaignsViewModel()
			{
				return GlobalContainer.Container.Resolve<CampaignsViewModel>();
			}

			public static CampaignTileViewModel CampaignTileViewModel()
			{
				return GlobalContainer.Container.Resolve<CampaignTileViewModel>();
			}

			public static CampaignAdvancedViewModel CampaignAdvancedViewModel()
			{
				return GlobalContainer.Container.Resolve<CampaignAdvancedViewModel>();
			}

			public static CampaignEditorViewModel CampaignEditorViewModel()
			{
				return GlobalContainer.Container.Resolve<CampaignEditorViewModel>();
			}

			public static SuccessViewTileViewModel SuccessViewTileViewModel()
			{
				return GlobalContainer.Container.Resolve<SuccessViewTileViewModel>();
			}

			public static SuccessMonitorViewModel SuccessMoniterViewModel()
			{
				return GlobalContainer.Container.Resolve<SuccessMonitorViewModel>();
			}
		}

		public static class Proxies
		{
			public static ProxiesViewModel ProxiesViewModel()
			{
				return GlobalContainer.Container.Resolve<ProxiesViewModel>();
			}

			public static ProxyEditorViewModel ProxyEditorViewModel()
			{
				return GlobalContainer.Container.Resolve<ProxyEditorViewModel>();
			}
		}
	}
}