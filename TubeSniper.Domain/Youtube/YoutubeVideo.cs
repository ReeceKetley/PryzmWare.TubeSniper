namespace TubeSniper.Domain.Youtube
{
	public class YoutubeVideo
	{
		public YoutubeVideo(string id, string title, string thumbnailUrl, string channelName)
		{
			Id = id;
			Title = title;
			ThumbnailUrl = thumbnailUrl;
			ChannelName = channelName;
		}

		public string Id { get; }

		public string Title { get; }

		public string ThumbnailUrl { get; }

		public string ChannelName { get; }

		public string Url => "https://youtube.com/watch?v=" + Id;
	}
}