using TubeSniper.Core.Application.Proxies;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Infrastructure.Repositories
{
	public interface IProxyEntryMapper
	{
		ProxyEntryDto Map(ProxyEntry model);
		ProxyEntry Map(ProxyEntryDto dto);
	}
}