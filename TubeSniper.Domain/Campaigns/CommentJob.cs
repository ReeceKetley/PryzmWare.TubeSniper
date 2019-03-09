using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class CommentJob
	{
		public CommentJob(YoutubeAccount account, HttpProxy proxy, YoutubeVideo video, Comment comment, CommentMethod commentMethod)
		{
			Account = account;
			Proxy = proxy;
			Video = video;
			Comment = comment;
			CommentMethod = commentMethod;
		}

		public YoutubeAccount Account { get; }
		public HttpProxy Proxy { get; }
		public YoutubeVideo Video { get; }
		public Comment Comment { get; }
		public CommentMethod CommentMethod { get; }
	}
}