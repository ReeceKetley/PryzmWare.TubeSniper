using System;
using System.Collections.Generic;
using Com.CloudRail.SI.Types;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces;
using TubeSniper.Infrastructure.Common;

namespace TubeSniper.Infrastructure.Services
{
	public class SearchService : ISearchService
	{
		public void Init()
		{
			YouTubeApi.VerifyApi();
		}

		public List<YoutubeVideo> SearchVideos(string query, int maxResults)
		{
			var videos = new List<VideoMetaData>();

			var offset = 0;
			for (; ; )
			{
				List<VideoMetaData> vids;
				try
				{
					vids = YouTubeApi.YouTubeClient.SearchVideos(query, offset, maxResults);
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
			var youtubeVideos = new List<YoutubeVideo>();
			foreach (var videoMetaData in videos)
			{
				youtubeVideos.Add(new YoutubeVideo(videoMetaData.GetId(), videoMetaData.GetTitle(), videoMetaData.GetThumbnailUrl(), videoMetaData.GetChannelId()));
			}
			return youtubeVideos;
		}

		public YoutubeVideo GetById(string id)
		{
			try
			{
				var videoMetaData = YouTubeApi.YouTubeClient.GetVideo(id);
				return new YoutubeVideo(videoMetaData.GetId(), videoMetaData.GetTitle(), videoMetaData.GetThumbnailUrl(), videoMetaData.GetChannelId());
			}
			catch
			{
				return null;
			}
		}
	}
}