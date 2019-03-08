using System.Net;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Campaigns
{
	public interface IJob
	{
		void Run(YoutubeAccount account, HttpProxy proxy, YoutubeVideo video, CommentRegister comment, bool asReply);
	}
}