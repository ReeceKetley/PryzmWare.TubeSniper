using System;
using System.Collections.Generic;
using TubeSniper.Domain.Accounts;

namespace TubeSniper.Domain.Interfaces.Persistence
{
	public interface IAccountEntryRepository
	{
		AccountEntry GetById(Guid id);
		AccountEntry GetByIdOrDefault(Guid id);
		IEnumerable<AccountEntry> GetAll();
		void Insert(AccountEntry model);
		void Delete(AccountEntry model);
		void Update(AccountEntry model);
	}
}
