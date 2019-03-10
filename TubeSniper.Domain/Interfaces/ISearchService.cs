using System.Collections.Generic;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Interfaces
{
	public interface ISearchService
	{
		void Init();
		List<YoutubeVideo> SearchVideos(Keyword keyword, int maxResults = 50);
	}
}