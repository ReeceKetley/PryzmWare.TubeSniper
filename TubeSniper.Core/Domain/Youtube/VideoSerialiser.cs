using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using TubeSniper.Core.Common.Helpers;

namespace TubeSniper.Core.Domain.Youtube
{
    public static class VideoSerialiser
    {
        public static  List<Video> FromJArray(JArray array)
        {
            var list = new List<Video>();
            foreach (var item in array)
            {
                var video = FromJObject(item.ToObject<JObject>());
                if (video == null)
                {
                    continue;
                }

                list.Add(video);
            }

            return list;
        }

        public static Video FromJObject(JObject source)
        {
            if (source["id"] == null || source["id"].Type != JTokenType.String)
            {
                return null;
            }

            var id = (string)source["id"];

            if (source["title"] == null || source["title"].Type != JTokenType.String)
            {
                return null;
            }

            var title = (string)source["title"];

            if (source["chanel_name"] == null || source["chanel_name"].Type != JTokenType.String)
            {
                return null;
            }

            var chanelName = (string)source["chanel_name"];

            if (source["channel_url"] == null || source["channel_url"].Type != JTokenType.String)
            {
                return null;
            }

            var channelUrl = (string)source["channel_url"];

            if (source["views"] == null || source["views"].Type != JTokenType.String)
            {
                return null;
            }

            var views = (string)source["views"];

            if (source["duration"] == null || source["duration"].Type != JTokenType.String)
            {
                return null;
            }

            var duration = (string)source["duration"];
            var split = duration.Split(':');
            if (split[0].Length == 1)
            {
                duration = "0" + duration;
            }
            if (split.Length == 2)
            {
                duration = "00:" + duration;
            }

            if (source["date"] == null || source["date"].Type != JTokenType.String)
            {
                return null;
            }

            var date = (string)source["date"];

            return new Video(id, title, chanelName, channelUrl, views, DateTime.ParseExact(duration, "hh:mm:ss", CultureInfo.InvariantCulture).TimeOfDay, GeneralHelpers.YouTubeDateToDateTime(date));
        }
    }
}