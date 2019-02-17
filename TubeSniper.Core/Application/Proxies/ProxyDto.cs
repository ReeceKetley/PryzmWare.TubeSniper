using System;

namespace TubeSniper.Core.Application.Proxies
{
	public class ProxyDto
	{
		public Guid Id { get; set; }
		public string Host { get; set; }
		public string Country { get; set; }
		public int ResponseTime { get; set; }
		public bool Ssl { get; set; }
		public int UseCount { get; set; }
		public DateTime? Tested { get; set; }
	}
}
