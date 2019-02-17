using System;
using System.Net;

namespace TubeSniper.Core.Domain.Proxies
{
    public class ProxyEntry
    {
	    public Guid Id { get; set; }
	    public WebProxy Proxy { get; set; }
	    public int UseCount { get; set; }
	    public bool InUse { get; set; }

	    public ProxyEntry DeepClone()
	    {
		    return (ProxyEntry) MemberwiseClone();
	    }
    }
}