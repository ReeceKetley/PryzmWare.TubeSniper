using System;

namespace TubeSniper.Domain.Proxies
{
	public class ProxyEntry
	{
		public ProxyEntry(Guid id, HttpProxy proxy)
		{
			Id = id;
			Proxy = proxy;
		}

		public ProxyEntry()
		{
		}

		public Guid Id { get; set; }
		public HttpProxy Proxy { get; set; }

		public ProxyEntry DeepClone()
		{
			return (ProxyEntry) MemberwiseClone();
		}
	}
}