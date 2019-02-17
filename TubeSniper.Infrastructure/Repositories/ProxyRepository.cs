using System;
using System.Collections.Generic;
using LiteDB;
using TubeSniper.Core.Application.Proxies;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Infrastructure.Repositories
{
	public class ProxyRepository : IProxyRepository
	{
		private LiteDatabase _database = new LiteDatabase(@"data\core.dat");

		public ProxyRepository()
		{
		}

		public void Insert(ProxyEntry proxy)
		{
			if (proxy.Id == Guid.Empty)
			{
				proxy.Id = Guid.NewGuid();
			}

			var proxyDto = proxy.ToProxyDto();
			var collection = _database.GetCollection<ProxyDto>("proxies");
			collection.Insert(proxyDto);
		}

		public void Delete(ProxyEntry proxy)
		{
			var proxyDto = proxy.ToProxyDto();
			var collection = _database.GetCollection<ProxyDto>("proxies");
			collection.Delete(proxyDto.Id);
		}

		public void Update(ProxyEntry proxy)
		{
			var proxyDto = proxy.ToProxyDto();
			var collection = _database.GetCollection<ProxyDto>("proxies");
			collection.Update(proxyDto);
		}

		public IEnumerable<ProxyEntry> GetAll()
		{
			var collection = _database.GetCollection<ProxyDto>("proxies");
			var list = new List<ProxyEntry>();
			foreach (var proxyDto in collection.FindAll())
			{
				list.Add(proxyDto.ToProxyEntry());
			}

			return list;
		}

		public ProxyEntry GetById(Guid id)
		{
			var collection = _database.GetCollection<ProxyDto>("proxies");
			var proxyDto = collection.FindOne(x => x.Id == id);
			return proxyDto.ToProxyEntry();
		}

		public ProxyEntry GetByIdOrDefault(Guid id)
		{
			var collection = _database.GetCollection<ProxyDto>("proxies");
			var proxyDto = collection.FindOne(x => x.Id == id);
			return proxyDto.ToProxyEntry();
		}
	}
}

