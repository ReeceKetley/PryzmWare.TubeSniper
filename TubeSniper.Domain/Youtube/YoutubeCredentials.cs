using System;

namespace TubeSniper.Domain.Youtube
{
	public class YoutubeCredentials
	{
		public YoutubeCredentials(YoutubeUsername username, YoutubePassword password)
		{
			CheckIsValid(username, password, true);
			Username = username;
			Password = password;
		}

		public YoutubeUsername Username { get; }

		public YoutubePassword Password { get; }

		public static bool TryCreate(YoutubeUsername username, YoutubePassword password, out YoutubeCredentials credentials)
		{
			if (!CheckIsValid(username, password, false))
			{
				credentials = null;
				return false;
			}

			credentials = new YoutubeCredentials(username, password);
			return true;
		}

		private static bool CheckIsValid(YoutubeUsername username, YoutubePassword password, bool throwExceptions)
		{
			if (username == null)
			{
				if (throwExceptions)
				{
					throw new ArgumentNullException(nameof(username));
				}

				return false;
			}

			if (password == null)
			{
				if (throwExceptions)
				{
					throw new ArgumentNullException(nameof(password));
				}

				return false;
			}

			return true;
		}
	}
}