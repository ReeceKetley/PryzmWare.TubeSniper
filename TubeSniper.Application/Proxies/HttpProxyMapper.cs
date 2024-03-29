﻿using System.Collections.Generic;
using System.Linq;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Proxies
{
	public class HttpProxyMapper : IHttpProxyMapper
	{
		public ProxyEntryDto Map(ProxyEntry model)
		{
			var dto = new ProxyEntryDto();
			dto.Id = model.Id;
			dto.Proxy = Map(model.Proxy);
			return dto;
		}

		public ProxyEntry Map(ProxyEntryDto dto)
		{
			HttpProxy proxy = null;
			if (dto.Proxy != null)
			{
				proxy = Map(dto.Proxy);
			}

			return new ProxyEntry(dto.Id, proxy);
		}

		public HttpProxy Map(HttpProxyDto dto)
		{
			HttpProxyAddress address = null;
			if (!string.IsNullOrEmpty(dto.Address) && !HttpProxyAddress.TryCreate(dto.Address, out address))
			{
				return null;
			}

			HttpProxyCredentials credentials = null;
			if (!string.IsNullOrEmpty(dto.CredentialsUsername) && !string.IsNullOrEmpty(dto.CredentialsPassword))
			{
				if (!HttpProxyCredentials.TryCreate(dto.CredentialsUsername, dto.CredentialsPassword, out credentials))
				{
					return null;
				}
			}

			return new HttpProxy(address, credentials);
		}

		public HttpProxyDto Map(HttpProxy model)
		{
			var dto = new HttpProxyDto();
			dto.Address = model.Address.Host + ":" + model.Address.Port;
			dto.CredentialsUsername = model.Credentials?.Username;
			dto.CredentialsPassword = model.Credentials?.Password;
			return dto;
		}

		public List<HttpProxy> Map(IEnumerable<HttpProxyDto> dtos)
		{
			return dtos.Select(Map).Where(model => model != null).ToList();
		}

		public List<HttpProxyDto> Map(IEnumerable<HttpProxy> models)
		{
			return models.Select(Map).Where(item => item != null).ToList();
		}
	}
}