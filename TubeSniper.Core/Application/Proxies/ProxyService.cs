using System;
using System.Collections.Generic;
using System.IO;
using TubeSniper.Core.Application.Events.Proxies;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Proxies
{
	public class ProxyService : IProxyService
	{
		private readonly IProxyRepository _proxyRepository;
		private readonly IProxyPortationMapper _proxyPortationMapper;

		public ProxyService(IProxyRepository proxyRepository, IProxyPortationMapper proxyPortationMapper)
		{
			_proxyRepository = proxyRepository;
			_proxyPortationMapper = proxyPortationMapper;
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

		public List<HttpProxy> ImportFromFile(string path)
		{
			string data;
			try
			{
				data = File.ReadAllText(path);
			}
			catch (Exception)
			{
				return null;
			}

			return _proxyPortationMapper.Map(data);
		}
	}
}
