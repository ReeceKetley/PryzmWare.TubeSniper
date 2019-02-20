using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class VideoProcessedEventArgs
	{
		public VideoProcessedEventArgs(YoutubeVideo video, string comment)
		{
			Video = video;
			Comment = comment;
		}

		public YoutubeVideo Video { get; }

		public string Comment { get; }
	}
}