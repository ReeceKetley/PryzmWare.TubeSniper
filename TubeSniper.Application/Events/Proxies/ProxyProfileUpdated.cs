using System;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Events.Proxies
{
	public class ProxyProfileUpdated : EventArgs
	{
		public ProxyProfileUpdated(ProxyEntry proxy)
		{
			Proxy = proxy;
		}

		public ProxyEntry Proxy { get; }
	}
}