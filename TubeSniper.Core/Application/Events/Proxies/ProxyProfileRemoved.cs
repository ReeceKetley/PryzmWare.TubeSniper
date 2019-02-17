using System;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Events.Proxies
{
	public class ProxyProfileRemoved : EventArgs
	{
		public ProxyEntry Proxy { get; }

		public ProxyProfileRemoved(ProxyEntry proxy)
		{
			Proxy = proxy;
		}
	}
}