using System;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Events.Accounts
{
	public class AccountProfileUpdated : EventArgs
	{
		public AccountProfileUpdated(YoutubeAccount account)
		{
			Account = account;
		}

		public YoutubeAccount Account { get; }
	}
}