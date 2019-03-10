using System;
using System.Collections.Generic;
using System.IO;
using TubeSniper.Application.Events.Proxies;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Proxies
{
	public class ProxyService : IProxyService
	{
		private readonly IProxyEntryRepository _proxyEntryRepository;
		private readonly IProxyPortationMapper _proxyPortationMapper;

		public ProxyService(IProxyEntryRepository proxyEntryRepository, IProxyPortationMapper proxyPortationMapper)
		{
			_proxyEntryRepository = proxyEntryRepository;
			_proxyPortationMapper = proxyPortationMapper;
		}

		public void Delete(Guid id)
		{
			var model = _proxyEntryRepository.GetById(id);
			_proxyEntryRepository.Delete(model);
			ProxyEvents.RaiseProxyProfileRemoved(new ProxyProfileRemoved(model));
		}

		public void Insert(ProxyEntry proxyEntry)
		{
			_proxyEntryRepository.Insert(proxyEntry);
			var clone = proxyEntry.DeepClone();
			ProxyEvents.RaiseProxyProfileCreated(new ProxyProfileCreated(clone));
		}

		public void Update(ProxyEntry proxyEntry)
		{
			_proxyEntryRepository.Update(proxyEntry);
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