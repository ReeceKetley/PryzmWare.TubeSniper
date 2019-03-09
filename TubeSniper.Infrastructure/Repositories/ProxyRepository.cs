using System;
using System.Collections.Generic;
using LiteDB;
using TubeSniper.Application.Proxies;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Infrastructure.Repositories
{
	public class ProxyRepository : IProxyRepository
	{
		private readonly IProxyEntryMapper _proxyEntryMapper;
		private readonly LiteDatabase _database = new LiteDatabase(@"data\core.dat");

		public ProxyRepository(IProxyEntryMapper proxyEntryMapper)
		{
			_proxyEntryMapper = proxyEntryMapper;
		}

		public void Insert(ProxyEntry proxy)
		{
			if (proxy.Id == Guid.Empty)
			{
				proxy.Id = Guid.NewGuid();
			}

			var proxyDto = _proxyEntryMapper.Map(proxy);
			var collection = _database.GetCollection<ProxyEntryDto>("proxies");
			collection.Insert(proxyDto);
		}

		public void Delete(ProxyEntry proxy)
		{
			var proxyDto = _proxyEntryMapper.Map(proxy);
			var collection = _database.GetCollection<ProxyEntryDto>("proxies");
			collection.Delete(proxyDto.Id);
		}

		public void Update(ProxyEntry proxy)
		{
			var proxyDto = _proxyEntryMapper.Map(proxy);
			var collection = _database.GetCollection<ProxyEntryDto>("proxies");
			collection.Update(proxyDto);
		}

		public IEnumerable<ProxyEntry> GetAll()
		{
			var collection = _database.GetCollection<ProxyEntryDto>("proxies");
			var list = new List<ProxyEntry>();
			foreach (var proxyDto in collection.FindAll())
			{
				list.Add(_proxyEntryMapper.Map(proxyDto));
			}

			return list;
		}

		public ProxyEntry GetById(Guid id)
		{
			var collection = _database.GetCollection<ProxyEntryDto>("proxies");
			var proxyDto = collection.FindOne(x => x.Id == id);
			return _proxyEntryMapper.Map(proxyDto);
		}

		public ProxyEntry GetByIdOrDefault(Guid id)
		{
			var collection = _database.GetCollection<ProxyEntryDto>("proxies");
			var proxyDto = collection.FindOne(x => x.Id == id);
			return _proxyEntryMapper.Map(proxyDto);
		}
	}

	public class ProxyEntryMapper : IProxyEntryMapper
	{
		private readonly IHttpProxyMapper _httpProxyMapper;

		public ProxyEntryMapper(IHttpProxyMapper httpProxyMapper)
		{
			_httpProxyMapper = httpProxyMapper;
		}

		public ProxyEntry Map(ProxyEntryDto dto)
		{
			HttpProxy proxy = null;
			if (dto.Proxy != null)
			{
				proxy = _httpProxyMapper.Map(dto.Proxy);
				if (proxy == null)
				{
					return null;
				}
			}

			return new ProxyEntry(dto.Id, proxy);
		}

		public ProxyEntryDto Map(ProxyEntry model)
		{
			var dto = new ProxyEntryDto();
			dto.Id = model.Id;
			if (model.Proxy != null)
			{
				dto.Proxy = _httpProxyMapper.Map(model.Proxy);
			}

			return dto;
		}
	}
}

