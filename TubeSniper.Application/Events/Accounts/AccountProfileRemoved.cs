using System;
using TubeSniper.Domain.Accounts;

namespace TubeSniper.Application.Events.Accounts
{
	public class AccountProfileRemoved : EventArgs
	{
		public AccountProfileRemoved(AccountEntry accountEntry)
		{
			AccountEntry = accountEntry;
		}

		public AccountEntry AccountEntry { get; }
	}
}