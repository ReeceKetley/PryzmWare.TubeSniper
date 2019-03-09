using System.Collections.Generic;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Interfaces
{
    public interface ISearchService
    {
        List<YoutubeVideo> SearchVideos(string searchString, int maxResults = 50);
	    YoutubeVideo GetById(string id);
	    void Init();
    }
}
