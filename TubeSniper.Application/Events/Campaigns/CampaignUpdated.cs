using System;
using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Application.Events.Campaigns
{
	public class CampaignUpdated : EventArgs
	{
		public CampaignUpdated(Campaign campaign)
		{
			Campaign = campaign;
		}

		public Campaign Campaign { get; }
	}
}