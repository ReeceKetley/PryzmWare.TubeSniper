using System.Net;

namespace TubeSniper.Core.Domain.Youtube
{
    public class LoadSignInResult
    {
        public LoadSignInResultCode Code { get; }
        public HttpStatusCode? Status { get; }

        public LoadSignInResult(LoadSignInResultCode code, HttpStatusCode? status)
        {
            Code = code;
            Status = status;
        }

        public LoadSignInResult()
        {
            Code = LoadSignInResultCode.Success;
        }

        public LoadSignInResult(LoadSignInResultCode httpError)
        {
            Code = httpError;
        }
    }
}