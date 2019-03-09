using System;
using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Application.Events.Campaigns
{
	public class CampaignRemoved : EventArgs
	{
		public CampaignRemoved(Campaign campaign)
		{
			Campaign = campaign;
		}

		public Campaign Campaign { get; }
	}
}