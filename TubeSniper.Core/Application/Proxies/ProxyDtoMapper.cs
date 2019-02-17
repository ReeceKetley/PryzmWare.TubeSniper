using System;
using System.Net;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Proxies
{
	public static class ProxyDtoMapper
	{
		public static ProxyDto ToProxyDto(this ProxyEntry proxy)
		{
			var proxyDto = new ProxyDto();
			proxyDto.Id = proxy.Id;
			proxyDto.Host = proxy.Proxy?.Address.ToString();
			proxyDto.UseCount = proxy.UseCount;
			return proxyDto;
		}

		public static ProxyEntry ToProxyEntry(this ProxyDto proxyDto)
		{
			var proxy = new ProxyEntry();
			if (string.IsNullOrEmpty(proxyDto.Host))
			{
				throw new ArgumentNullException("proxyDto", "Proxy host is null or empty");
			}
			proxy.Proxy = new WebProxy(new Uri(proxyDto.Host));
			proxy.Id = proxyDto.Id;
			return proxy;
		}
	}
}
