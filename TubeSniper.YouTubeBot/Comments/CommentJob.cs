using System.Net;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.Comments
{
	public class CommentJob
	{
		public CommentJob(YoutubeAccount account, WebProxy proxy, VideoId videoId, Comment comment, CommentMethod commentMethod)
		{
			Account = account;
			Proxy = proxy;
			VideoId = videoId;
			Comment = comment;
			CommentMethod = commentMethod;
		}

		public YoutubeAccount Account { get; }
		public WebProxy Proxy { get; }
		public VideoId VideoId { get; }
		public Comment Comment { get; }
		public CommentMethod CommentMethod { get; }
	}
}