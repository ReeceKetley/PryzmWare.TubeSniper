using System.Collections.Generic;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Campaigns
{
	public class ThreadSafeVideoStack
	{
		private readonly object _mutex = new object();
		private readonly List<YoutubeVideo> _videos;
		private int _index = 0;

		public ThreadSafeVideoStack(List<YoutubeVideo> videos)
		{
			_videos = videos;
		}

		public YoutubeVideo Next()
		{
			lock (_mutex)
			{
				if (_index >= _videos.Count)
				{
					return null;
				}

				var video = _videos[_index];
				++_index;
				return video;
			}
		}
	}
}