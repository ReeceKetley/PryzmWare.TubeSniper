using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Presentation.Wpf.Commands;

namespace TubeSniper.Presentation.Wpf.ViewModels.Auth
{
	public class AuthViewModel : ViewModelBase, IDataErrorInfo
	{
		private readonly IAuthService _authService;
		private ICommand _submitKeyCommand;
		private readonly AuthViewModelValidator _validator;

		public AuthViewModel(IAuthService authService)
		{
			_authService = authService;
			_validator = new AuthViewModelValidator();
		}

		public ICommand SubmitKeyCommand => _submitKeyCommand ?? (_submitKeyCommand = new RelayCommand(SubmitKey, CanSubmitKey));

		public event EventHandler Submitted;

		public bool IsVisible { get; set; }

		public string LicenseKey { get; set; }

		public ProductKey Key { get; set; }

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
				var results = _validator.Validate(this);
				if (results != null && results.Errors.Any())
				{
					var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
					return errors;
				}

				return string.Empty;
			}
		}

		private void SubmitKey(object obj)
		{
			Key = new ProductKey(LicenseKey);
			OnSubmitted();
		}

		private bool CanSubmitKey(object obj)
		{
			return _validator.Validate(this).IsValid;
		}

		protected virtual void OnSubmitted()
		{
			Submitted?.Invoke(this, EventArgs.Empty);
		}
	}
}