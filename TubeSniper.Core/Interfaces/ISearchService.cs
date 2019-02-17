using System.Collections.Generic;
using Com.CloudRail.SI.Types;

namespace TubeSniper.Core.Interfaces
{
    public interface ISearchService
    {
        List<VideoMetaData> SearchVideos(string searchString, int maxResults = 50);
        VideoMetaData GetById(string id);
    }
}
