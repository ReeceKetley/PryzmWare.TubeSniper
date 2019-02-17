using TubeSniper.Core.Domain.Campaigns;

namespace TubeSniper.Core.Application.Campaigns
{
	public interface ICampaignMappper
	{
		CampaignDto Map(Campaign campaign);
		Campaign Map(CampaignDto campaignDto);
	}
}