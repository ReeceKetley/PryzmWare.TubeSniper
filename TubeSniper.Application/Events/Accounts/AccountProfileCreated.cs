using System;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Events.Accounts
{
	public class AccountProfileCreated : EventArgs
	{
		public AccountProfileCreated(YoutubeAccount account)
		{
			Account = account;
		}

		public YoutubeAccount Account { get; }
	}
}