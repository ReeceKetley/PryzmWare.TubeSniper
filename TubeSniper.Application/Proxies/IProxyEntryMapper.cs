using TubeSniper.Domain.Proxies;

namespace TubeSniper.Application.Proxies
{
	public interface IProxyEntryMapper
	{
		ProxyEntryDto Map(ProxyEntry model);
		ProxyEntry Map(ProxyEntryDto dto);
	}
}