using System.Collections.Generic;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Proxies
{
	public interface IHttpProxyMapper
	{
		HttpProxy Map(HttpProxyDto dto);
		ProxyEntry Map(ProxyEntryDto dto);
		HttpProxyDto Map(HttpProxy model);
		ProxyEntryDto Map(ProxyEntry model);
		List<HttpProxy> Map(IEnumerable<HttpProxyDto> dtos);
		List<HttpProxyDto> Map(IEnumerable<HttpProxy> models);
	}
}