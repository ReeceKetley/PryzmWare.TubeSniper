using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using TubeSniper.Core.Application.Events.Proxies;
using TubeSniper.Core.Application.Proxies;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.Proxies.Views;
using TubeSniper.Presentation.Wpf.ViewModels;

namespace TubeSniper.Presentation.Wpf.Proxies.ViewModels
{
	public class ProxiesViewModel : ViewModelBase
	{
		private static Regex _proxyRegex = new Regex(@"((?:\d{1,3}\.){3}(?:\d{1,3}))(?:\s?[,:]?\s?)(\d{1,5})", RegexOptions.Compiled);

		private readonly IProxyRepository _proxyRepository;
		private readonly IProxyService _proxyService;
		private ICommand _createProxyCommand;
		private ICommand _importProxyCommand;

		public ProxiesViewModel(IProxyRepository proxyRepository, IProxyService proxyService)
		{
			_proxyRepository = proxyRepository;
			_proxyService = proxyService;
			Initialise();
		}


		public ObservableCollection<ProxyListItem> Proxies { get; } = new ObservableCollection<ProxyListItem>();
		public ObservableCollection<ProxyListItem> SelectedItems { get; } = new ObservableCollection<ProxyListItem>();
		public ICommand CreateProxyCommand => _createProxyCommand ?? (_createProxyCommand = new RelayCommand(CreateProxy));
		public ICommand ImportProxyCommand => _importProxyCommand ?? (_importProxyCommand = new RelayCommand(ImportProxy));

		private void Delete()
		{
		}

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
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Import Proxies (Regex Matched)";
			openFileDialog.ShowDialog();
			if (!File.Exists(openFileDialog.FileName))
			{
				// Shouldnt happen but worth checking.
				MessageBoxResult result = System.Windows.MessageBox.Show("There was an error accessing that file do you have the correct privileges? ",
					"Warning",
					MessageBoxButton.OK,
					MessageBoxImage.Error);
				return;
			}

			var file = File.ReadAllLines(openFileDialog.FileName);
			foreach (var s in file)
			{
				var proxy = s.Split(':');
				if (proxy.Length >= 1)
				{
					var url = "http://" + proxy[0] + ":" + proxy[1];
					if (proxy.Length >= 3)
					{
						url = url + ":" + proxy[2];
					}

					if (proxy.Length >= 4)
					{
						url = url + proxy[3];
					}

					if (!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var isProxy))
					{
						continue;
					}

					var proxyEntry = new ProxyEntry();
					proxyEntry.Proxy = new WebProxy(isProxy);
					_proxyRepository.Insert(proxyEntry);
				}
			}
		}

		private void CreateProxy(object obj)
		{
			var viewModel = ViewModelFactory.Proxies.ProxyEditorViewModel();
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