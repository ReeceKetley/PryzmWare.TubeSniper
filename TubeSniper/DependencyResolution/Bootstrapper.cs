using SimpleInjector;
using TubeSniper.Core.Application.Accounts;
using TubeSniper.Core.Application.Campaigns;
using TubeSniper.Core.Application.Proxies;
using TubeSniper.Core.Common;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Interfaces;
using TubeSniper.Core.Interfaces.Persistence;
using TubeSniper.Core.Services;
using TubeSniper.Infrastructure.Repositories;
using TubeSniper.Infrastructure.Services;
using TubeSniper.Presentation.Wpf.Openers;

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
			Container.Register<IProxyService, ProxyService>(Lifestyle.Singleton);
			Container.Register<ICampaignMappper, CampaignMappper>(Lifestyle.Singleton);
			Container.Register<IAuthService, AuthService>(Lifestyle.Singleton);
			//Container.Register<ISuccessfulCommentManager, SuccessfulCommentManager>(Lifestyle.Singleton);
			Container.Register<MainWindowOpener>();
			Container.Verify();
		}

		public static void Main(string[] args)
		{
			var handler = Container.GetInstance<MainWindowOpener>();
			handler.StartApplication();
		}
	}
}
