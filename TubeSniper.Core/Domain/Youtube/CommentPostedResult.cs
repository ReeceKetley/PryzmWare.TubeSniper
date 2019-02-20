namespace TubeSniper.Core.Domain.Youtube
{
	public class CommentPostedResult
	{
		public CommentPostedResult(YoutubeVideo video, string comment, CommentPostedResultCode code)
		{
			Code = code;
			Video = video;
			Comment = comment;
		}

		public CommentPostedResultCode Code { get; }

		public YoutubeVideo Video { get; }

		public string Comment { get; }
	}
}