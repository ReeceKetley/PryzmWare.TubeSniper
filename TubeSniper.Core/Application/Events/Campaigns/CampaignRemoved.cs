using System;
using TubeSniper.Core.Domain.Campaigns;

namespace TubeSniper.Core.Application.Events.Campaigns
{
	public class CampaignRemoved : EventArgs
	{
		public Campaign Campaign { get; }

		public CampaignRemoved(Campaign campaign)
		{
			Campaign = campaign;
		}
	}
}