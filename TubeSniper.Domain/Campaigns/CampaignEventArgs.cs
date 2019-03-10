using System;

namespace TubeSniper.Domain.Campaigns
{
	public class CampaignEventArgs : EventArgs
	{
		public CampaignEventArgs(Campaign campaign)
		{
			Campaign = campaign;
		}

		public Campaign Campaign { get; }
	}
}