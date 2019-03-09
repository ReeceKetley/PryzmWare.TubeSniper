namespace TubeSniper.Domain.Youtube
{
	public class AccountRegisterItem
	{
		public YoutubeAccount Account { get; }
		public int UseCount { get; set; }
		public bool InUse { get; set; }

		public AccountRegisterItem(YoutubeAccount account)
		{
			Account = account;
		}
	}
}