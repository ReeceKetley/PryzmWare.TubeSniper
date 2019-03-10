using TubeSniper.Domain.Accounts;

namespace TubeSniper.Domain.Youtube
{
	public class AccountRegisterItem
	{
		public AccountRegisterItem(AccountEntry accountEntry)
		{
			AccountEntry = accountEntry;
		}

		public AccountEntry AccountEntry { get; }
		public int UseCount { get; set; }
		public bool InUse { get; set; }
	}
}