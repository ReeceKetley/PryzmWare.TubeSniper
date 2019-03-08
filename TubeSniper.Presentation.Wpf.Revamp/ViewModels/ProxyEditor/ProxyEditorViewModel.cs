using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows.Input;
using System.Windows.Markup;
using TubeSniper.Core.Application.Proxies;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Validators.ProxyEditor;

namespace TubeSniper.Presentation.Wpf.ViewModels.ProxyEditor
{
	public class ProxyEditorViewModel : ViewModelBase, IDataErrorInfo
	{
		private readonly IProxyService _proxyService;
		private ICommand _submitCommand;
		private readonly ProxyEditorViewModelValidator _validator;

		public ProxyEditorViewModel(IProxyService proxyService)
		{
			_proxyService = proxyService;
			_validator = new ProxyEditorViewModelValidator();
		}

		public ICommand SubmitCommand => _submitCommand ?? (_submitCommand = new RelayCommand(Submit, CanSubmit));

		public Action CloseAction { get; set; }

		public string ProxyString { get; set; }

		[DependsOn(nameof(ProxyPassword))]
		public string ProxyUsername { get; set; }

		[DependsOn(nameof(ProxyUsername))]
		public string ProxyPassword { get; set; }

		public string this[string columnName]
		{
			get
			{
				var firstOrDefault = _validator.Validate(this).Errors.FirstOrDefault(x => x.PropertyName == columnName);
				if (firstOrDefault != null)
				{
					return _validator != null ? firstOrDefault.ErrorMessage : "";
				}

				return "";
			}
		}

		public string Error
		{
			get
			{
				var results = _validator?.Validate(this);
				if (results != null && results.Errors.Any())
				{
					var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
					return errors;
				}

				return string.Empty;
			}
		}

		private void Submit(object obj)
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
			proxyEntry.Proxy = new HttpProxy(new HttpProxyAddress(proxyString), ProxyUsername != null ? new HttpProxyCredentials(ProxyUsername, ProxyPassword): null); 
			_proxyService.Insert(proxyEntry);

			CloseAction?.Invoke();
		}

		private bool CanSubmit(object obj)
		{
			return _validator.Validate(this).IsValid;
		}
	}
}