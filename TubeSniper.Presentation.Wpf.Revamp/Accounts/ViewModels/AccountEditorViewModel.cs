using System;
using System.Windows.Input;
using TubeSniper.Core.Application.Accounts;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.ViewModels;

namespace TubeSniper.Presentation.Wpf.Accounts.ViewModels
{
	public class AccountEditorViewModel : ViewModelBase
	{
		public string Email { get; set; } = "";
		public string Password { get; set; } = "";
		public string RecoveryEmail { get; set; } = "";
		private readonly IAccountService _accountService;
		private ICommand _saveCommand;
		public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save));
		public Action CloseAction { get; set; }

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
		}
	}
}
