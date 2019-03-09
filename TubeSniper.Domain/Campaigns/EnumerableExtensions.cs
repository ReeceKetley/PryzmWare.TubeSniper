using System;
using System.Collections.Generic;
using System.Linq;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public static class EnumerableExtensions
	{
		public static List<YoutubeVideo> Randomize(this List<YoutubeVideo> target)
		{
			Random r = new Random();
			return new List<YoutubeVideo>(target.OrderBy(x => (r.Next())));
		}
	}
}