using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Youtube
{
	public interface IYoutubeCommentBotFactory
	{
		IYoutubeCommentBot Create(VirtualBrowser.VirtualBrowser browser);
	}
}