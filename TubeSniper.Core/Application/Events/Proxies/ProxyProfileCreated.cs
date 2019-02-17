using System;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Events.Proxies
{
	public class ProxyProfileCreated : EventArgs
	{
		public ProxyEntry Proxy { get; }

		public ProxyProfileCreated(ProxyEntry proxy)
		{
			Proxy = proxy;
		}
	}
}