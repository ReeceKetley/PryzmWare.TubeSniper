namespace TubeSniper.YouTubeBot.Youtube
{
	public class YoutubeAccount
	{
		public YoutubeCredentials Credentials { get; }
		public string RecoveryEmail { get; }

		public YoutubeAccount(YoutubeCredentials credentials, string recoveryEmail)
		{
			Credentials = credentials;
			RecoveryEmail = recoveryEmail;

		}
	}
}
