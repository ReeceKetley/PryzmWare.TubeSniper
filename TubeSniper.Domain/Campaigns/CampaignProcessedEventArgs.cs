using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class CampaignProcessedEventArgs
	{
		public CampaignMeta CampaignMeta { get; }
		public YoutubeVideo Video { get; }
		public string Comment { get; }

		public CampaignProcessedEventArgs(YoutubeVideo video, string comment, CampaignMeta campaignMeta)
		{
			CampaignMeta = campaignMeta;
			Video = video;
			Comment = comment;
		}
	}
}