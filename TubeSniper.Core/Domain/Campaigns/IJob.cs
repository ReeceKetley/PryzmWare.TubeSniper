using System.Net;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Campaigns
{
	public interface IJob
	{
		void Run(YoutubeAccount account, WebProxy proxy, YoutubeVideo video, CommentRegister comment, bool asReply);
	}
}