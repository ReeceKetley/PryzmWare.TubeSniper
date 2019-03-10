using TubeSniper.Domain.Accounts;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class CommentJob
	{
		public CommentJob(AccountEntry accountEntry, HttpProxy proxy, YoutubeVideo video, Comment comment, CommentMethod commentMethod)
		{
			AccountEntry = accountEntry;
			Proxy = proxy;
			Video = video;
			Comment = comment;
			CommentMethod = commentMethod;
		}

		public AccountEntry AccountEntry { get; }
		public HttpProxy Proxy { get; }
		public YoutubeVideo Video { get; }
		public Comment Comment { get; }
		public CommentMethod CommentMethod { get; }
	}
}