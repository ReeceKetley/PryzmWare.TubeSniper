using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Application.Campaigns
{
	public interface ICampaignMapper
	{
		CampaignDto Map(Campaign campaign);
		Campaign Map(CampaignDto campaignDto);
	}
}