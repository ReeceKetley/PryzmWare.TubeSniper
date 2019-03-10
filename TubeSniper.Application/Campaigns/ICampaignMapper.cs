using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Application.Campaigns
{
	public interface ICampaignMapper
	{
		Campaign Map(CampaignDto dto);
		CampaignDto Map(Campaign model);
	}
}