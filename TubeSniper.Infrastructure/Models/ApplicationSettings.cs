using System;

namespace TubeSniper.Infrastructure.Models
{
	[Serializable]
	public class ApplicationSettings
	{
		public int MinTypingSpeed { get; set; }
		public int MaxTypingSpeed { get; set; }

	}
}
