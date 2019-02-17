using System;
using TubeSniper.Core.Domain.Campaigns;

namespace TubeSniper.Core.Application.Events.Campaigns
{
	public class CampaignCreated : EventArgs
	{
		public Campaign Campaign { get; }

		public CampaignCreated(Campaign campaign)
		{
			Campaign = campaign;
		}
	}
}