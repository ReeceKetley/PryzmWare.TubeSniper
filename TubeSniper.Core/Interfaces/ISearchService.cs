using System.Collections.Generic;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Interfaces
{
    public interface ISearchService
    {
        List<YoutubeVideo> SearchVideos(string searchString, int maxResults = 50);
	    YoutubeVideo GetById(string id);
	    void Init();
    }
}
