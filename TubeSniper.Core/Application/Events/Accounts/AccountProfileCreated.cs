using System;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Application.Events.Accounts
{
	public class AccountProfileCreated : EventArgs
	{
		public YoutubeAccount Account { get; }

		public AccountProfileCreated(YoutubeAccount account)
		{
			Account = account;
		}
	}
}