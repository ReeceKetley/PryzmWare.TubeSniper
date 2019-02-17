using System;
using TubeSniper.Core.Domain.Campaigns;

namespace TubeSniper.Core.Application.Events.Campaigns
{
	public class CampaignUpdated : EventArgs
	{
		public Campaign Campaign { get; }

		public CampaignUpdated(Campaign campaign)
		{
			Campaign = campaign;
		}
	}
}