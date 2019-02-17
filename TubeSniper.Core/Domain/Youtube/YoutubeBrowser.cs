using System.Collections.Generic;
using System.Net;
using Com.CloudRail.SI.Types;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Interfaces;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Youtube
{
    public class YoutubeBrowser
    {
        private ILoginService _loginService;
        private YoutubeVideoPage _videoPage;
        private readonly ISearchService _searchService;
        public VirtualBrowser Browser { get; }
        public WebProxy Proxy { get; }


        public YoutubeBrowser(WebProxy proxy, YoutubeAccount account, bool useCache = false)
        {
            Proxy = proxy;
            Browser = VirtualBrowser.Create(proxy, account, useCache);
        }

        public LoginResultCode Login(YoutubeAccount credentials)
        {
            var loginResult = _loginService.Login(Browser, credentials).Code;
            return loginResult;
        }


        public bool LoadVideoById(string videoId)
        {
            return Browser.LoadUrlAndVerify("https://www.youtube.com/watch?v=" + videoId);
        }

        public List<VideoMetaData> SearchVideos(string searchTerms, int maxResults)
        {
            return _searchService.SearchVideos(searchTerms, maxResults);
        }

        public CommentPostedResult PostComment(YoutubeBrowser browser, string id, string comment, bool asReply)
        {
            _videoPage = new YoutubeVideoPage(browser);
            if (!LoadVideoById(id))
            {
                return null;
            }
            if (!_videoPage.PostComment(comment, asReply))
            {
                return new CommentPostedResult(SearchService.GetById(id), comment, CommentPostedResultCode.Failure);
            }

            return new CommentPostedResult(SearchService.GetById(id), comment, CommentPostedResultCode.Success);
        }
    }
}
