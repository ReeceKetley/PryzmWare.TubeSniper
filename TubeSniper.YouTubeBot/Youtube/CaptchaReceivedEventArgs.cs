using System;

namespace TubeSniper.YouTubeBot.Youtube
{
	public class CaptchaReceivedEventArgs
	{
		public Uri ImageUrl { get; }
		public string Solution { get; set; }

		public CaptchaReceivedEventArgs(Uri imageUrl)
		{
			ImageUrl = imageUrl;
		}
	}
}