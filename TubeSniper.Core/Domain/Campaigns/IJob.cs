using System.Net;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Campaigns
{
    public interface IJob
    {
        void Run(YoutubeAccount account, WebProxy proxy, string videoId, CommentRegister comment, bool asReply);
    }
}