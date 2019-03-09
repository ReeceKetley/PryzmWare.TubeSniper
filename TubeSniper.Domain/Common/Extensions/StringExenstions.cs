using System.Collections.Generic;
using System.Linq;

namespace TubeSniper.Domain.Common.Extensions
{
	public static class StringExenstions
	{
		public static IEnumerable<string> Split(this string str, int chunkSize)
		{
			return Enumerable.Range(0, str.Length / chunkSize)
				.Select(i => str.Substring(i * chunkSize, chunkSize));
		}

	}
}
