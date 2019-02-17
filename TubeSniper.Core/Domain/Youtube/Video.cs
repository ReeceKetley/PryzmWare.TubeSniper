using System;

namespace TubeSniper.Core.Domain.Youtube
{
    public class Video
    {
        public string Id { get; }
        public string Title { get; }
        public string ChannelName { get; }
        public string ChannelUrl { get; }
        public string Views { get; }
        public TimeSpan Duration { get; }
        public DateTime? Date { get; }

        public Video(string id, string title, string channelName, string channelUrl, string views, TimeSpan duration, DateTime? date)
        {
            Id = id.Replace("/watch?v=", "");
            Title = title;
            ChannelName = channelName;
            ChannelUrl = channelUrl;
            Views = views;
            Duration = duration;
            Date = date;
        }
    }
}