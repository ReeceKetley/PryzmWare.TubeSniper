using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public interface ICommentPoster
	{
		void Run(YoutubeAccount account, HttpProxy proxy, YoutubeVideo video, CommentGenerator comment, bool asReply, ICaptchaService captchaService);
	}
}