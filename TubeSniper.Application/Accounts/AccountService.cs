using System;
using TubeSniper.Application.Events.Accounts;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Accounts
{
	public class AccountService : IAccountService
	{
		private readonly IAccountsRepository _accountsRepository;

		public AccountService(IAccountsRepository accountsRepository)
		{
			_accountsRepository = accountsRepository;
		}

		public void Delete(Guid id)
		{
			var model = _accountsRepository.GetById(id);
			_accountsRepository.Delete(model);
			AccountEvents.RaiseAccountProfileRemoved(new AccountProfileRemoved(model));
		}

		public void Insert(YoutubeAccount account)
		{
			_accountsRepository.Insert(account);
			var clone = account.DeepClone();
			AccountEvents.RaiseAccountProfileCreated(new AccountProfileCreated(clone));
		}

		public void Update(YoutubeAccount account)
		{
			_accountsRepository.Update(account);
			AccountEvents.RaiseAccountProfileUpdated(new AccountProfileUpdated(account.DeepClone()));
		}
	}
}