using System;

namespace TubeSniper.Domain.Campaigns
{
	public class CampaignEventArgs : EventArgs
	{
		public Campaign Campaign { get; }

		public CampaignEventArgs(Campaign campaign)
		{
			Campaign = campaign;
		}
	}
}