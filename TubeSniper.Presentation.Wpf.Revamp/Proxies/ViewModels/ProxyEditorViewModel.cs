using System;
using System.Net;
using System.Windows.Input;
using TubeSniper.Core.Application.Proxies;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.ViewModels;

namespace TubeSniper.Presentation.Wpf.Proxies.ViewModels
{
	public class ProxyEditorViewModel : ViewModelBase
	{
		private readonly IProxyService _proxyService;
		private ICommand _saveCommand;

		public ProxyEditorViewModel(IProxyService proxyService)
		{
			_proxyService = proxyService;
		}

		public string ProxyString { get; set; } = "";
		public string ProxyUsername { get; set; }
		public string ProxyPassword { get; set; }
		public Action CloseAction { get; set; }
		public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save));

		private void Save(object obj)
		{
			if (string.IsNullOrEmpty(ProxyString))
			{
				return;
			}

			var proxyString = ProxyString;
			if (!string.IsNullOrEmpty(ProxyUsername))
			{
				proxyString = proxyString + ":" + ProxyUsername;
			}

			if (!string.IsNullOrEmpty(ProxyPassword))
			{
				proxyString = proxyString + ":" + ProxyPassword;
			}

			var proxyEntry = new ProxyEntry();
			proxyEntry.Proxy = new WebProxy(new Uri("http://" + proxyString + "/"));
			_proxyService.Insert(proxyEntry);

			CloseAction?.Invoke();
		}
	}
}