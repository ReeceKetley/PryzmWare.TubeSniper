using System.Net;

namespace TubeSniper.YouTubeBot.Auth
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
	}
}