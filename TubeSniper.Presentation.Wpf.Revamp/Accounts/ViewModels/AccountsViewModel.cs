using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TubeSniper.Application.Accounts;
using TubeSniper.Application.Events.Accounts;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Presentation.Wpf.Accounts.Views;
using TubeSniper.Presentation.Wpf.Commands;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.ViewModels;

namespace TubeSniper.Presentation.Wpf.Accounts.ViewModels
{
	public class AccountsViewModel : ViewModelBase
	{
		private readonly IAccountService _accountService;
		private readonly IAccountEntryRepository _accountEntryRepository;
		private ICommand _createAccount;

		public AccountsViewModel(IAccountEntryRepository accountEntryRepository, IAccountService accountService)
		{
			_accountEntryRepository = accountEntryRepository;
			_accountService = accountService;
			Initialise();
		}

		public ObservableCollection<AccountListItem> Accounts { get; } = new ObservableCollection<AccountListItem>();
		public ObservableCollection<AccountListItem> SelectedItems { get; } = new ObservableCollection<AccountListItem>();
		public ICommand CreateAccount => _createAccount ?? (_createAccount = new RelayCommand(Create));

		private void Create(object obj)
		{
			var accountEditViewModel = new AccountEditorViewModel(_accountService);
			var accountEditView = new AccountEditorView(accountEditViewModel);
			accountEditView.ShowDialog();
		}

		public void Initialise()
		{
			foreach (var youtubeAccount in _accountEntryRepository.GetAll())
			{
				var viewModel = ViewModelLocator.Locate<AccountListItem>();
				viewModel.SetAccount(youtubeAccount);
				Accounts.Add(viewModel);
			}

			AccountEvents.AccountProfileCreated += AccountEvents_OnAccountProfileCreated;
			AccountEvents.AccountProfileUpdated += AccountEvents_OnAccountProfileUpdated;
			AccountEvents.AccountProfileRemoved += AccountEvents_OnAccountProfileRemoved;
		}

		private void AccountEvents_OnAccountProfileRemoved(object sender, AccountProfileRemoved e)
		{
			var viewModels = Accounts.Where(x => x.Id == e.AccountEntry.Id).ToList();
			foreach (var viewModel in viewModels)
			{
				Accounts.Remove(viewModel);
			}
		}

		private void AccountEvents_OnAccountProfileUpdated(object sender, AccountProfileUpdated e)
		{
			var viewModels = Accounts.Where(x => x.Id == e.AccountEntry.Id);
			foreach (var viewModel in viewModels)
			{
				viewModel.SetAccount(e.AccountEntry);
			}
		}

		private void AccountEvents_OnAccountProfileCreated(object sender, AccountProfileCreated e)
		{
			var viewModel = ViewModelLocator.Locate<AccountListItem>();
			viewModel.SetAccount(e.AccountEntry);
			Accounts.Add(viewModel);
		}
	}
}