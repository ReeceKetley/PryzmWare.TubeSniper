using System;

namespace TubeSniper.Application.Events.Proxies
{
	public static class ProxyEvents
	{
		public static event EventHandler<ProxyProfileCreated> ProxyProfileCreated;
		public static event EventHandler<ProxyProfileRemoved> ProxyProfileRemoved;
		public static event EventHandler<ProxyProfileUpdated> ProxyProfileUpdated;

		public static void RaiseProxyProfileCreated(ProxyProfileCreated e)
		{
			ProxyProfileCreated?.Invoke(null, e);
		}

		public static void RaiseProxyProfileRemoved(ProxyProfileRemoved e)
		{
			ProxyProfileRemoved?.Invoke(null, e);
		}

		public static void RaiseProxyProfileUpdated(ProxyProfileUpdated e)
		{
			ProxyProfileUpdated?.Invoke(null, e);
		}
	}
}