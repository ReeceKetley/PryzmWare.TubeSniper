namespace TubeSniper.YouTubeBot.Youtube
{
	public class VideoProcessedEventArgs
	{
		public VideoProcessedEventArgs(VideoId videoId, string comment)
		{
			VideoId = videoId;
			Comment = comment;
		}

		public VideoId VideoId { get; }

		public string Comment { get; }
	}
}