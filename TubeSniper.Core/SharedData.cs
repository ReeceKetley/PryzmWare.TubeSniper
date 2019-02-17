using System;
using TubeSniper.Core.Domain.Campaigns;

namespace TubeSniper.Core
{
	public static class SharedData
	{
		public static event EventHandler<CurrentStepEventArgs> CurrentStep;
		public static event EventHandler<bool> Loaded;
		public static event EventHandler<CampaignProcessedEventArgs> CampaignProcessed;

		public static void OnCurrentStep(CurrentStepEventArgs e)
		{
			CurrentStep?.Invoke(null, e);
		}

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
