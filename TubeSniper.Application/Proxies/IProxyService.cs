using System;
using System.Collections.Generic;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Proxies
{
	public interface IProxyService
	{
		void Delete(Guid id);
		void Insert(ProxyEntry proxyEntry);
		void Update(ProxyEntry proxyEntry);
		List<HttpProxy> ImportFromFile(string path);
	}
}