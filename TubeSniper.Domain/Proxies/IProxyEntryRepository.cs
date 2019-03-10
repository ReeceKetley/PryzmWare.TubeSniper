using System;
using System.Collections.Generic;

namespace TubeSniper.Domain.Proxies
{
	public interface IProxyEntryRepository
	{
		void Delete(ProxyEntry proxy);
		IEnumerable<ProxyEntry> GetAll();
		ProxyEntry GetById(Guid id);
		ProxyEntry GetByIdOrDefault(Guid id);
		void Insert(ProxyEntry proxy);
		void Update(ProxyEntry proxy);
	}
}