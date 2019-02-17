using Com.CloudRail.SI.Types;

namespace TubeSniper.Core.Domain.Campaigns
{
    public class VideoProcessedEventArgs
    {
        public VideoMetaData Meta { get; }
        public string Comment { get; }

        public VideoProcessedEventArgs(VideoMetaData meta, string comment)
        {
            Meta = meta;
            Comment = comment;
        }
    }

	public class   CampaignProcessedEventArgs
	{
		public CampaignMeta CampaignMeta { get; }
		public VideoMetaData Meta { get; }
		public string Comment { get; }

		public CampaignProcessedEventArgs(VideoMetaData meta, string comment, CampaignMeta campaignMeta)
		{
			CampaignMeta = campaignMeta;
			Meta = meta;
			Comment = comment;
		}
	}
}