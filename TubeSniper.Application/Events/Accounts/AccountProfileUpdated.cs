using System;
using TubeSniper.Domain.Accounts;

namespace TubeSniper.Application.Events.Accounts
{
	public class AccountProfileUpdated : EventArgs
	{
		public AccountProfileUpdated(AccountEntry accountEntry)
		{
			AccountEntry = accountEntry;
		}

		public AccountEntry AccountEntry { get; }
	}
}