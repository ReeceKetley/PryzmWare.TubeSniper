using System;
using TubeSniper.Core.Application.Events.Proxies;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Proxies
{
	public class ProxyService : IProxyService
	{
		private readonly IProxyRepository _proxyRepository;

		public ProxyService(IProxyRepository proxyRepository)
		{
			_proxyRepository = proxyRepository;
		}

		public void Delete(Guid id)
		{
			var model = _proxyRepository.GetById(id);
			_proxyRepository.Delete(model);
			ProxyEvents.RaiseProxyProfileRemoved(new ProxyProfileRemoved(model));
		}

		public void Insert(ProxyEntry proxyEntry)
		{
			_proxyRepository.Insert(proxyEntry);
			var clone = proxyEntry.DeepClone();
			ProxyEvents.RaiseProxyProfileCreated(new ProxyProfileCreated(clone));
		}

		public void Update(ProxyEntry proxyEntry)
		{
			_proxyRepository.Update(proxyEntry);
			ProxyEvents.RaiseProxyProfileUpdated(new ProxyProfileUpdated(proxyEntry.DeepClone()));
		}
	}
}
