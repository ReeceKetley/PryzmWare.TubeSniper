using System;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Application.Events.Accounts
{
	public class AccountProfileRemoved : EventArgs
	{
		public YoutubeAccount Account { get; }

		public AccountProfileRemoved(YoutubeAccount account)
		{
			Account = account;
		}
	}
}