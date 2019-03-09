using System.Collections.Generic;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Proxies
{
	public interface IProxyPortationMapper
	{
		string Map(IEnumerable<HttpProxy> proxies);
		List<HttpProxy> Map(string data);
	}
}