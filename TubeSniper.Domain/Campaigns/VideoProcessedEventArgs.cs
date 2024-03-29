﻿using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Campaigns
{
	public class VideoProcessedEventArgs
	{
		public VideoProcessedEventArgs(YoutubeVideo video, string comment)
		{
			Video = video;
			Comment = comment;
		}

		public YoutubeVideo Video { get; }

		public string Comment { get; }
	}
}