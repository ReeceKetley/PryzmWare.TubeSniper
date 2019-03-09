using System;

namespace TubeSniper.Domain.Youtube
{
	public class YoutubeAccount
	{
		public Guid Id { get; set; }
		public YoutubeCredentials Credentials { get; }
		public string RecoveryEmail { get; }

		public YoutubeAccount(YoutubeCredentials credentials, string recoveryEmail)
		{
			Credentials = credentials;
			RecoveryEmail = recoveryEmail;

		}

		public YoutubeAccount(YoutubeAccount source)
		{
			Id = source.Id;
			if (Credentials != null)
			{
				Credentials = new YoutubeCredentials(source.Credentials);
			}

			RecoveryEmail = source.RecoveryEmail;
		}

		public YoutubeAccount DeepClone()
		{
			return (YoutubeAccount) MemberwiseClone();
		}
	}
}