using System;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Youtube
{
    public class YoutubeVideoPage
    {
        private readonly YoutubeBrowser _youtubeBrowser;
        private readonly YoutubeCommentBot _commentBot;

        public YoutubeVideoPage(YoutubeBrowser youtubeBrowser)
        {
            _youtubeBrowser = youtubeBrowser;
            _commentBot = new YoutubeCommentBot();
        }

        public bool IsVideoPage()
        {
            if (!_youtubeBrowser.Browser.WebView.Url.Contains("watch?v="))
            {
                return false;
            }

            if (!_youtubeBrowser.Browser.WebView.ElementExists(".style-scope.ytd-video-primary-info-renderer", TimeSpan.FromSeconds(30)))
            {
                return false;
            }

            return true;
        }

        public bool PostComment(string videoId, string message, bool asReply)
        {
            if (!_youtubeBrowser.LoadVideoById(videoId))
            {
                return false;
            }

            if (_commentBot.PostComment(_youtubeBrowser.Browser, message, asReply, 5).Code != PostCommentResultCode.Success)
            {
                return false;
            }

            return true;
        }

        public bool PostComment(string message, bool asReply)
        {
            if (_commentBot.PostComment(_youtubeBrowser.Browser, message, asReply, 5).Code != PostCommentResultCode.Success)
            {
                return false;
            }

            return true;
        }


    }
}