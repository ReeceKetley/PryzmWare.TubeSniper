namespace TubeSniper.Domain.Youtube
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

		public YoutubeCredentials(YoutubeCredentials source)
		{
			Email = source.Email;
			Password = source.Password;
		}
	}
}