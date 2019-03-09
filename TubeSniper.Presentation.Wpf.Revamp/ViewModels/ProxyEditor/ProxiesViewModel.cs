using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TubeSniper.Application.Events.Proxies;
using TubeSniper.Application.Proxies;
using TubeSniper.Application.Services;
using TubeSniper.Domain.Proxies;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.Views.ProxyEditor;

namespace TubeSniper.Presentation.Wpf.ViewModels.ProxyEditor
{
	public class ProxiesViewModel : ViewModelBase
	{
		private readonly IDialogService _dialogService;
		private readonly IMessageBoxService _messageBoxService;

		private readonly IProxyRepository _proxyRepository;
		private readonly IProxyService _proxyService;
		private ICommand _createProxyCommand;
		private ICommand _importProxyCommand;

		public ProxiesViewModel(IProxyRepository proxyRepository, IProxyService proxyService, IDialogService dialogService, IMessageBoxService messageBoxService)
		{
			_proxyRepository = proxyRepository;
			_proxyService = proxyService;
			_dialogService = dialogService;
			_messageBoxService = messageBoxService;
			Initialise();
		}


		public ObservableCollection<ProxyListItem> Proxies { get; } = new ObservableCollection<ProxyListItem>();
		public ObservableCollection<ProxyListItem> SelectedItems { get; } = new ObservableCollection<ProxyListItem>();
		public ICommand CreateProxyCommand => _createProxyCommand ?? (_createProxyCommand = new RelayCommand(CreateProxy));
		public ICommand ImportProxyCommand => _importProxyCommand ?? (_importProxyCommand = new RelayCommand(ImportProxy));

		private void Initialise()
		{
			foreach (var proxyEntry in _proxyRepository.GetAll())
			{
				var viewModel = ViewModelLocator.Locate<ProxyListItem>();
				viewModel.SetProxy(proxyEntry);
				Proxies.Add(viewModel);
			}

			ProxyEvents.ProxyProfileCreated += ProxyEvents_OnProxyProfileCreated;
			ProxyEvents.ProxyProfileRemoved += ProxyEvents_OnProxyProfileRemoved;
			ProxyEvents.ProxyProfileUpdated += ProxyEvents_OnProxyProfileUpdated;
		}

		private void ImportProxy(object obj)
		{
			var path = _dialogService.OpenDelimitedFile();
			if (path == null)
			{
				return;
			}

			var proxies = _proxyService.ImportFromFile(path);
			if (proxies == null)
			{
				return;
			}

			if (proxies.Count < 1)
			{
				_messageBoxService.OkayInformation("The selected file doesn't appear to contain any valid proxies.");
				return;
			}

			foreach (var proxy in proxies)
			{
				var proxyEntry = new ProxyEntry();
				proxyEntry.Proxy = proxy;
				_proxyService.Insert(proxyEntry);
			}

			_messageBoxService.OkayInformation("Successfully imported proxy list!");
		}


		private void CreateProxy(object obj)
		{
			var viewModel = ViewModelLocator.Locate<ProxyEditorViewModel>();
			var view = new ProxyEditorView(viewModel);
			view.ShowDialog();
		}

		private void ProxyEvents_OnProxyProfileCreated(object sender, ProxyProfileCreated e)
		{
			var viewModel = ViewModelLocator.Locate<ProxyListItem>();
			viewModel.SetProxy(e.Proxy);
			Proxies.Add(viewModel);
		}

		private void ProxyEvents_OnProxyProfileRemoved(object sender, ProxyProfileRemoved e)
		{
			var viewModels = Proxies.Where(x => x.Id == e.Proxy.Id).ToList();
			foreach (var viewModel in viewModels)
			{
				Proxies.Remove(viewModel);
			}
		}

		private void ProxyEvents_OnProxyProfileUpdated(object sender, ProxyProfileUpdated e)
		{
			var viewModels = Proxies.Where(x => x.Id == e.Proxy.Id);
			foreach (var viewModel in viewModels)
			{
				viewModel.SetProxy(e.Proxy);
			}
		}
	}
}