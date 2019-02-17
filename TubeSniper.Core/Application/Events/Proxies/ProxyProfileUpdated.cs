using System;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Events.Proxies
{
	public class ProxyProfileUpdated : EventArgs
	{
		public ProxyEntry Proxy { get; }

		public ProxyProfileUpdated(ProxyEntry proxy)
		{
			Proxy = proxy;
		}
	}
}