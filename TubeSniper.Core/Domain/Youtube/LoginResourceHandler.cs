using System.IO;
using EO.WebBrowser;

namespace TubeSniper.Core.Domain.Youtube
{
    internal class LoginResourceHandler : ResourceHandler
    {
        public bool IsLoggedIn
        {
            get
            {
                lock (_mutex)
                {
                    return _isLoggedIn;

                }
            }
        }

        public bool RequestFinished
        {
            get
            {
                lock (_mutex)
                {
                    return _requestFinished;
                }
            }
        }
        private object _mutex = new object();
        private bool _isLoggedIn;
        private bool _requestFinished;

        public override bool Match(Request request)
        {
            //Console.WriteLine(request.Url);
            if (request.Url.Contains("/challenge?"))
            {
                return true;
            }

            return false;
        }

        public override void ProcessRequest(Request request, Response response)
        {
            base.ProcessRequest(request, response);
            using (var reader = new StreamReader(response.OutputStream))
            {
                var content = reader.ReadToEnd();
                lock (_mutex)
                {
                    if (content.Contains(".com/CheckCookie?"))
                    {
                        _isLoggedIn = true;
                    }

                    _requestFinished = true;
                }
            }   
        }


    }
}