using TubeSniper.Core.Interfaces;

namespace TubeSniper.Core.Domain.Campaigns
{
	class CampaignService
	{
		private readonly ICampaignService _campaignService;

		public CampaignService(ICampaignService campaignService)
		{
			_campaignService = campaignService;
		}

		public void StartCampaign(Campaign campaign)
		{
			campaign.Start();
			campaign.VideoProcessed += Campaign_VideoProcessed;
		}

		private void Campaign_VideoProcessed(object sender, VideoProcessedEventArgs e)
		{
			_campaignService.Update((Campaign) sender);
		}
	}
}
