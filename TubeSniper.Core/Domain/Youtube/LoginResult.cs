using System.Net;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Domain.Youtube
{
    public class LoginResult
    {
	    public HttpProxy Proxy { get; }
	    public LoginResultCode Code { get; }

        public LoginResult(LoginResultCode code, HttpProxy proxy = null)
        {
	        Proxy = proxy;
	        Code = code;
        }

        public LoginResult()
        {
            Code = LoginResultCode.Success;
        }
    }
}