namespace TubeSniper.Domain.Youtube
{
	public class YoutubeCommentBotFactory : IYoutubeCommentBotFactory
	{
		public IYoutubeCommentBot Create(VirtualBrowser.VirtualBrowser browser)
		{
			return new YoutubeCommentBot(browser);
		}
	}
}