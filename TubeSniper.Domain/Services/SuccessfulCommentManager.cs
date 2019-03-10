using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TubeSniper.Domain.Services
{
	public class SuccessComment
	{
		public string VideoTitle { get; set; }
		public string Comment { get; set; }
		public string Email { get; set; }
		public string Proxy { get; set; }
		public Bitmap Thumbnail { get; set; }
	}

	public static class SuccessfulCommentManager
	{
		private static readonly List<SuccessComment> SuccessfulList = new List<SuccessComment>();
		private static readonly object Mutex = new object();

		public static void Add(SuccessComment comment)
		{
			lock (Mutex)
			{
				SuccessfulList.Add(comment);
			}
		}

		public static List<SuccessComment> GetComments()
		{
			lock (Mutex)
			{
				return SuccessfulList.ToList();
			}
		}
	}
}