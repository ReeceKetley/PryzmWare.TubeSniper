using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Interfaces
{
    public interface ICommentBot
    {
        PostCommentResult PostComment(VirtualBrowser browser, string message, bool asReply, int watchDuration);
    }
}
