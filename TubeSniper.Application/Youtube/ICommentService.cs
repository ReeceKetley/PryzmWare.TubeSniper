using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Application.Youtube
{
	public interface ICommentService
	{
		CommentServiceResult PostComment(CommentJob commentJob);
	}
}