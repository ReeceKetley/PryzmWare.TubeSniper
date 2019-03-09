using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Domain.Youtube
{
	public interface IYoutubeCommentBot
	{
		PostCommentResult PostComment(CommentJob commentJob, int watchDuration);
	}
}