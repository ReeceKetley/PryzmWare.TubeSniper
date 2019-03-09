using System;
using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Domain
{
	public static class SharedData
	{
		public static event EventHandler<bool> Loaded;
		public static event EventHandler<CampaignProcessedEventArgs> CampaignProcessed;


		public static void OnCampaignProcessed(CampaignProcessedEventArgs e)
		{
			CampaignProcessed?.Invoke(null, e);
		}

		public static void OnLoaded(bool e)
		{
			Loaded?.Invoke(null, e);
		}
	}
}
