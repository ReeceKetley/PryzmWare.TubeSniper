using System;
using System.Collections.Generic;
using System.Linq;

namespace TubeSniper.Core.Domain.Proxies
{
	public class ProxyRegister : IProxyRegister
	{
		public readonly List<ProxyEntry> Proxies;

		public bool HasProxies;

		public ProxyRegister(List<ProxyEntry> proxies)
		{
			Proxies = proxies;
			if (proxies != null && proxies.Count != 0)
			{
				HasProxies = true;
			}
		}

		public ProxyEntry Aquire()
        {
            foreach (var proxyEntry in Proxies.OrderByDescending(x => x.UseCount).ToArray())
            {
	            if (proxyEntry.InUse)
	            {
					continue;
	            }
				proxyEntry.InUse = true;
				Update(proxyEntry);
	            return proxyEntry;
            }
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

    }


}
