using System;
using System.Windows.Input;
using TubeSniper.Application.Accounts;
using TubeSniper.Domain.Youtube;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.ViewModels;

namespace TubeSniper.Presentation.Wpf.Accounts.ViewModels
{
	public class AccountListItem : ViewModelBase
	{
		private readonly IAccountService _accountService;
		private ICommand _deleteCommand;
		private YoutubeAccount _accountEntry;
		public string Email { get; private set; }
		public string Password { get; private set; }
		public string Recovery { get; private set; }
		public Guid Id { get; private set; }
		public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));

		public AccountListItem(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public void Delete(object obj)
		{
			_accountService.Delete(_accountEntry.Id);
		}

		public void SetAccount(YoutubeAccount account)
		{
		Email = account.Credentials.Email;
		Password = account.Credentials.Password;
		Recovery = account.RecoveryEmail;
		Id = account.Id;
			_accountEntry = account;
		}
	}
}