using System;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Proxies
{
	public interface IProxyService
	{
		void Delete(Guid id);
		void Insert(ProxyEntry proxyEntry);
		void Update(ProxyEntry proxyEntry);
	}
}