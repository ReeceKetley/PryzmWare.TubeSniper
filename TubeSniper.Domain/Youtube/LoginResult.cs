using TubeSniper.Domain.Proxies;

namespace TubeSniper.Domain.Youtube
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
    }
}