using SimpleInjector;
using TubeSniper.Application.Accounts;
using TubeSniper.Application.Campaigns;
using TubeSniper.Application.Proxies;
using TubeSniper.Application.Services;
using TubeSniper.Domain.Auth;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Common;
using TubeSniper.Domain.Interfaces;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Infrastructure.Repositories;
using TubeSniper.Infrastructure.Services;
using TubeSniper.Presentation.Wpf.Openers;
using TubeSniper.Presentation.Wpf.Services;

namespace TubeSniper.DependencyResolution
{
	internal class Bootstrapper
	{
		private static readonly Container Container;

		static Bootstrapper()
		{
			Container = new Container();
			GlobalContainer.Container = new MyContainer(Container);
			//Container.Register<EntryPoint>();
			Container.Register<ICampaignService, CampaignService>(Lifestyle.Singleton);
			Container.Register<ICampaignRepository, CampaignRepository>(Lifestyle.Singleton);
			Container.Register<IAccountsRepository, AccountsRepository>(Lifestyle.Singleton);
			Container.Register<IAccountService, AccountService>(Lifestyle.Singleton);
			Container.Register<IProxyRepository, ProxyRepository>(Lifestyle.Singleton);
			Container.Register<IProxyEntryMapper, ProxyEntryMapper>(Lifestyle.Singleton);
			Container.Register<IProxyService, ProxyService>(Lifestyle.Singleton);
			Container.Register<IHttpProxyMapper, HttpProxyMapper>(Lifestyle.Singleton);
			Container.Register<IProxyPortationMapper, ProxyPortationMapper>(Lifestyle.Singleton);
			Container.Register<ICampaignMapper, CampaignMapper>(Lifestyle.Singleton);
			Container.Register<IAuthService, AuthService>(Lifestyle.Singleton);
			Container.Register<ISearchService, SearchService>(Lifestyle.Singleton);
			Container.Register<ICaptchaService, CaptchaService>(Lifestyle.Singleton);
			Container.Register<IProxyTestService, ProxyTestService>(Lifestyle.Singleton);
			//Container.Register<IVirtualBrowser, VirtualBrowser>(Lifestyle.Singleton);
			Container.Register<MainWindowOpener>();

			Container.Register<IDialogService, DialogService>(Lifestyle.Singleton);
			Container.Register<IMessageBoxService, MessageBoxService>(Lifestyle.Singleton);
			Container.Register<IClipboardService, ClipboardService>(Lifestyle.Singleton);
			Container.Verify();
		}

		public static void Main(string[] args)
		{
			var handler = Container.GetInstance<MainWindowOpener>();
			handler.StartApplication();
		}
	}
}
