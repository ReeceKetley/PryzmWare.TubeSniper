﻿using System;
using System.Collections.Generic;
using Com.CloudRail.SI.Types;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces;
using TubeSniper.Domain.Youtube;
using TubeSniper.Infrastructure.Common;

namespace TubeSniper.Infrastructure.Services
{
	public class SearchService : ISearchService
	{
		public void Init()
		{
			YouTubeApi.VerifyApi();
		}

		public List<YoutubeVideo> SearchVideos(Keyword keyword, int maxResults)
		{
			var videos = new List<VideoMetaData>();

			var offset = 0;
			for (; ; )
			{
				List<VideoMetaData> result;
				try
				{
					result = YouTubeApi.YouTubeClient.SearchVideos(keyword.Value, offset, maxResults);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}

				if (result == null || result.Count < 1)
				{
					break;
				}

				offset += result.Count;
				videos.AddRange(result);
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
	}
}
