using System;
using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Application.Events.Campaigns
{
	public class CampaignCreated : EventArgs
	{
		public CampaignCreated(Campaign campaign)
		{
			Campaign = campaign;
		}

		public Campaign Campaign { get; }
	}
}