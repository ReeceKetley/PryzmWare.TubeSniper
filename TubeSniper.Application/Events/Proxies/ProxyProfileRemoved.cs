using System;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Events.Proxies
{
	public class ProxyProfileRemoved : EventArgs
	{
		public ProxyProfileRemoved(ProxyEntry proxy)
		{
			Proxy = proxy;
		}

		public ProxyEntry Proxy { get; }
	}
}