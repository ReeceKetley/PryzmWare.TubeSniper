using System;
using TubeSniper.Core.Application.Events.Accounts;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces.Persistence;

namespace TubeSniper.Core.Application.Accounts
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
