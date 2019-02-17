using System;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Application.Events.Accounts
{
	public class AccountProfileUpdated : EventArgs
	{
		public YoutubeAccount Account { get; }

		public AccountProfileUpdated(YoutubeAccount account)
		{
			Account = account;
		}
	}
}