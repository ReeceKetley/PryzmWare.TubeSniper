using System;
using System.Collections.Generic;

namespace TubeSniper.Domain.Proxies
{
	public class ProxyCollection : IProxyRegister
	{
		public readonly List<ProxyEntry> Proxies;

		public bool HasProxies;

		public ProxyCollection(List<ProxyEntry> proxies)
		{
			Proxies = proxies;
			if (proxies != null && proxies.Count != 0)
			{
				HasProxies = true;
			}
		}

		public ProxyEntry Aquire()
        {
			var random = new Random();
            return Proxies[random.Next(0, Proxies.Count - 1)];
        }

	    public void Update(ProxyEntry proxy)
	    {
		    foreach (var proxyEntry in Proxies.ToArray())
		    {
			    if (proxyEntry.Proxy == proxy.Proxy)
			    {
				    Proxies.Remove(proxyEntry);
					Proxies.Add(proxy);
			    }
		    }
	    }

		public void Remove(ProxyEntry proxy)
	    {
		    foreach (var proxyEntry in Proxies.ToArray())
		    {
			    if (proxyEntry.Proxy == proxy.Proxy)
			    {
				    Proxies.Remove(proxyEntry);
			    }
		    }
	    }

    }


}
