using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using TubeSniper.Application.Accounts;
using TubeSniper.Domain.Youtube;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Validators.AccountEditor;
using TubeSniper.Presentation.Wpf.ViewModels;

namespace TubeSniper.Presentation.Wpf.Accounts.ViewModels
{
	public class AccountEditorViewModel : ViewModelBase, IDataErrorInfo
	{
		public string Email { get; set; } = "";
		public string Password { get; set; } = "";
		public string RecoveryEmail { get; set; } = "";
		private readonly IAccountService _accountService;
		private readonly AccountEditorViewModelValidator _validator;
		private ICommand _saveCommand;
		public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save));
		public Action CloseAction { get; set; }

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

		private void Save(object obj)
		{
			if (string.IsNullOrEmpty(Email))
			{
				return;
			}

			if (string.IsNullOrEmpty(Password))
			{
				return;
			}

			if (string.IsNullOrEmpty(RecoveryEmail))
			{
				return;
			}

			var credentials = new YoutubeCredentials(Email, Password);
			var youtubeAccount = new YoutubeAccount(credentials, RecoveryEmail);
			_accountService.Insert(youtubeAccount);
			CloseAction?.Invoke();
		}

		public AccountEditorViewModel(IAccountService accountService)
		{
			_accountService = accountService;
			_validator = new AccountEditorViewModelValidator();
		}
	}
}
