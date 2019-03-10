using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.YouTubeBot.Comments
{
	public class CommentPostedResult
	{
		public CommentPostedResult(VideoId video, string comment, CommentPostedResultCode code)
		{
			Code = code;
			Video = video;
			Comment = comment;
		}

		public CommentPostedResultCode Code { get; }

		public VideoId Video { get; }

		public string Comment { get; }
	}
}