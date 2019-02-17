using System;
using System.Collections.Generic;
using System.Threading;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces;

namespace TubeSniper.Core.Services
{
    public class YoutubeCommentBot : ICommentBot
    {
        public PostCommentResult PostComment(VirtualBrowser browser, string message, bool asReply, int watchDuration)
        {
            if (!LoadComments(browser))
            {
                return new PostCommentResult(PostCommentResultCode.ObjectNotFound);
            }

            //browser.WebView.RegisterResourceHandler(new CommentResourceHandler(browser.Proxy, browser));

            if (!ActivateCommentBox(browser, asReply))
            {
                return new PostCommentResult(PostCommentResultCode.Failure);
            }

//            if (!GetAllCommentIds(browser))
//            {
//                return new PostCommentResult(PostCommentResultCode.ObjectNotFound);
//            }

            if (!SubmitComment(browser, message, watchDuration))
            {
                return new PostCommentResult(PostCommentResultCode.Failure);
            }

            return new PostCommentResult(PostCommentResultCode.Success);

        }


/*
        public class CommentResourceHandler : ResourceHandler
        {
            private readonly WebProxy _proxy;
            private readonly VirtualBrowser _browser;

            public CommentResourceHandler(WebProxy proxy, VirtualBrowser browser) : base(90, 10240)
            {
                _proxy = proxy;
                _browser = browser;
            }

            public override bool Match(Request request)
            {
                if (request.Url.Contains("https://www.youtube.com/service_ajax?name=createComment"))
                {
                    return true;
                }

                return base.Match(request);
            }

            public override void ProcessRequest(Request request, Response response)
            {
                var http = new RestClient(request.Url);
                http.Proxy = _proxy;
                var restRequest = new RestRequest(Method.POST);
                http.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36";
                foreach (string key in request.Headers.Keys)
                {
                    restRequest.AddHeader(key, request.Headers[key]);
                    //Console.WriteLine("{0} - {1}", key, request.Headers[key]);
                }

                CookieCollection cookie = _browser.WebView.Engine.CookieManager.GetCookies();
                for (int i = 0; i < cookie.Count; i++)
                {
                    restRequest.AddCookie(cookie[i].Name, cookie[i].Value);
                }

                restRequest.AddParameter("application/x-www-form-urlencoded", request.PostData); 

                var restResponse = http.Execute(restRequest);
                //Console.WriteLine("REST RESPONSE: " + restResponse.Content);
                response.Write(restResponse.Content);
                base.ProcessRequest(request, response);
            }
        }
*/

        private bool LoadComments(VirtualBrowser browser)
        {
            var endTime = DateTime.Now + TimeSpan.FromSeconds(30);
            for (; ; Thread.Sleep(250))
            {
                if (DateTime.Now > endTime)
                {
                    return false;
                }

                browser.WebView.ScrollToElement("#comments");
                if (!browser.WebView.GetEvalBool("$(\"ytd-comment-thread-renderer\").eq(0).length > 0"))
                {
                    continue;
                }

                return true;
            }
        }

        private bool ActivateCommentBox(VirtualBrowser browser, bool asReply)
        {
            if (!asReply)
            {
                if (!MoveToCommentBox(browser))
                {
                    return false;
                }

            }
            else
            {
                if (!ClickReplyToTopComment(browser))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ClickReplyToTopComment(VirtualBrowser browser)
        {
            browser.WebView.EvalScript("$(\"ytd-comment-thread-renderer\").eq(0).find(\".ytd-button-renderer .style-scope.ytd-button-renderer.style-text\").eq(0).click();", true);
            if (!browser.WebView.GetEvalBool("document.activeElement != undefined && document.activeElement.id == \"textarea\""))
            {
                return false;
            }

            return true;
        }

        private bool MoveToCommentBox(VirtualBrowser browser)
        {
            browser.WebView.ClickElement("#placeholder-area");
            return true;
        }

        private bool SubmitComment(VirtualBrowser browser, string message, int watchDuration)
        {
            var template = CommentTemplate.FromString(message);
            Random random = new Random();
            Thread.Sleep(random.Next(watchDuration / 2, watchDuration) * 3000);
            browser.Keyboard.TypeString(template.Generate(new Dictionary<string, string>()));
            Thread.Sleep(1000);
            browser.Keyboard.PressTab();
            Thread.Sleep(100);
            browser.Keyboard.PressTab();
            Thread.Sleep(150);
            browser.Keyboard.PressSubmit();
            Thread.Sleep(3000);
            return true;
        }
    }
}
