using System;
using TubeSniper.Domain.Accounts;

namespace TubeSniper.Application.Accounts
{
	public interface IAccountService
	{
		void Delete(Guid id);
		void Insert(AccountEntry accountEntry);
		void Update(AccountEntry accountEntry);
	}
}