using System;
using System.Windows.Input;
using TubeSniper.Core.Application.Proxies;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Presentation.Wpf.Commands;

namespace TubeSniper.Presentation.Wpf.ViewModels.ProxyEditor
{
	public class ProxyListItem : ViewModelBase
	{
		private readonly IProxyService _proxyService;
		private ICommand _deleteCommand;
		private ProxyEntry _proxyEntry;

		public ProxyListItem(IProxyService proxyService)
		{
			_proxyService = proxyService;
		}

		public string Host => _proxyEntry?.Proxy.Address.Host;
		public int Port => _proxyEntry.Proxy.Address.Port;
		public string Country { get; set; }
		public int ResponseTime { get; set; }
		public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));
		public Guid Id { get; protected set; }

		public void Delete(object obj)
		{
			_proxyService.Delete(_proxyEntry.Id);
		}

		public void SetProxy(ProxyEntry model)
		{
			Id = model.Id;
			_proxyEntry = model;
		}
	}
}