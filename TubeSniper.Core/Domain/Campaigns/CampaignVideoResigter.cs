using System.Collections.Generic;
using Com.CloudRail.SI.Types;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class CampaignVideoResigter
	{
		private readonly List<VideoMetaData> _videos;
		private int currentVideoIndex = 0;
		private readonly object _mutex = new object();

		public CampaignVideoResigter(List<VideoMetaData> videos)
		{
			_videos = videos;
		}

		public VideoMetaData Next()
		{
			lock (_mutex)
			{
				if (currentVideoIndex >= _videos.Count)
				{
					return null;
				}

				var video = _videos[currentVideoIndex];
				++currentVideoIndex;
				return video;
			}
		}
	}
}
