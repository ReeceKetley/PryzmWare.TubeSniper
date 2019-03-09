using System;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Events.Proxies
{
	public class ProxyProfileCreated : EventArgs
	{
		public ProxyProfileCreated(ProxyEntry proxy)
		{
			Proxy = proxy;
		}

		public ProxyEntry Proxy { get; }
	}
}