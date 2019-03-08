using System;

namespace TubeSniper.Core.Application.Proxies
{
	public class ProxyEntryDto
	{
		public Guid Id { get; set; }
		public HttpProxyDto Proxy { get; set; }
	}
}
