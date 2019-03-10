namespace TubeSniper.YouTubeBot.Youtube
{
	public class YoutubeCredentials
	{
		public string Email { get; }
		public string Password { get; }

		public YoutubeCredentials(string email, string password)
		{
			Email = email.Trim();
			Password = password;
		}

		public YoutubeCredentials DeepClone()
		{
			return (YoutubeCredentials)MemberwiseClone();
		}
	}
}
