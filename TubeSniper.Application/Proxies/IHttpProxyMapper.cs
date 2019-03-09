using System.Collections.Generic;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Proxies
{
	public interface IHttpProxyMapper
	{
		HttpProxy Map(HttpProxyDto dto);
		HttpProxyDto Map(HttpProxy model);
		List<HttpProxy> Map(IEnumerable<HttpProxyDto> dtos);
		List<HttpProxyDto> Map(IEnumerable<HttpProxy> models);
	}
}