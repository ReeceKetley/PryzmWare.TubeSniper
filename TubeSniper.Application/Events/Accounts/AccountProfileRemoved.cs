using System;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Events.Accounts
{
	public class AccountProfileRemoved : EventArgs
	{
		public AccountProfileRemoved(YoutubeAccount account)
		{
			Account = account;
		}

		public YoutubeAccount Account { get; }
	}
}