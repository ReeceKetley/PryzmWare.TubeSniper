using System.Collections.Generic;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class CampaignMeta
	{
		public CampaignMeta()
		{
		}

		public CampaignMeta(CampaignMeta source)
		{
			Title = source.Title;
			if (source.Accounts != null)
			{
				Accounts = new List<YoutubeAccount>();
				foreach (var account in source.Accounts)
				{
					Accounts.Add(new YoutubeAccount(account));
				}
			}

			if (source.VideoProccesed != null)
			{
				VideoProccesed = new List<string>(source.VideoProccesed);
			}

			SearchTerm = source.SearchTerm;
		}

		public string Title { get; set; }
		public List<YoutubeAccount> Accounts { get; set; } = new List<YoutubeAccount>();
		public ProxyRegister ProxyRegister { get; set; }
		public List<string> VideoProccesed { get; set; } = new List<string>();
		public string SearchTerm { get; set; }
	}
}