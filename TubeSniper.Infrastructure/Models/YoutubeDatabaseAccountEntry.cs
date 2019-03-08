using System;

namespace TubeSniper.Infrastructure.Models
{
    [Serializable]
    public class YoutubeDatabaseAccountEntry
    {
        public string Recovery { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
	    public int UseCount { get; set; }
    }
}
