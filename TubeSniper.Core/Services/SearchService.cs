using System;
using System.Collections.Generic;
using Com.CloudRail.SI.Types;

namespace TubeSniper.Core.Services
{
	public static class SearchService
	{
		// TODO Move to infrastructure 
		// TODO Make own VideoMeta class
		// TODO Move to own Amazon API Bridge
		private static YouTubeApi _server;
		private static bool isInit;
		public static bool Init()
		{
			if (isInit)
			{
				return true;
			}
			Console.WriteLine("Init api...");
			if (!YouTubeApi.VerifyApi())
			{
				Console.WriteLine("Init failed...");
				return false;
			}
			Console.WriteLine("API Init success....");
			isInit = true;
			return true;
		}
		public static List<VideoMetaData> SearchVideos(string query, int maxResults)
		{
			if (!isInit)
			{
				Init();
			}

			List<VideoMetaData> videos = new List<VideoMetaData>();
		
			var offset = 0;
			for (; ; )
			{
				List<VideoMetaData> vids;
				try
				{
					vids = YouTubeApi.YouTubeClient.SearchVideos(query, offset, 100);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}

				if (vids == null || vids.Count < 1)
				{
					break;
				}
				offset += vids.Count;
				videos.AddRange(vids);
				if (videos.Count >= maxResults)
				{
					break;
				}
			}

			return videos;
		}

		public static VideoMetaData GetById(string id)
		{
			try
			{
				return YouTubeApi.YouTubeClient.GetVideo(id);
			}
			catch
			{
				return null;
			}
		}
	}
}