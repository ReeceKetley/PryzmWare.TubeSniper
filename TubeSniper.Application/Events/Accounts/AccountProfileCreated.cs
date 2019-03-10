using System;
using TubeSniper.Domain.Accounts;

namespace TubeSniper.Application.Events.Accounts
{
	public class AccountProfileCreated : EventArgs
	{
		public AccountProfileCreated(AccountEntry accountEntry)
		{
			AccountEntry = accountEntry;
		}

		public AccountEntry AccountEntry { get; }
	}
}