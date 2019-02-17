using System.Net;

namespace TubeSniper.Core.Domain.Youtube
{
    public class LoginResult
    {
	    public WebProxy Proxy { get; }
	    public LoginResultCode Code { get; }

        public LoginResult(LoginResultCode code, WebProxy proxy = null)
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