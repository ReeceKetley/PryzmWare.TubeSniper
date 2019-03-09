using System;

namespace TubeSniper.Application.Events.Campaigns
{
	public static class CampaignEvents
	{
		public static event EventHandler<CampaignCreated> CampaignCreated;
		public static event EventHandler<CampaignRemoved> CampaignRemoved;
		public static event EventHandler<CampaignUpdated> CampaignUpdated;

		public static void RaiseCampaignCreated(CampaignCreated e)
		{
			CampaignCreated?.Invoke(null, e);
		}

		public static void RaiseCampaignRemoved(CampaignRemoved e)
		{
			CampaignRemoved?.Invoke(null, e);
		}

		public static void RaiseCampaignUpdated(CampaignUpdated e)
		{
			CampaignUpdated?.Invoke(null, e);
		}
	}
}