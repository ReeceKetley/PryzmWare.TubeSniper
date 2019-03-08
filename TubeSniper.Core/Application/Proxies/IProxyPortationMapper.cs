using System.Collections.Generic;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Proxies
{
    public interface IProxyPortationMapper
    {
        string Map(IEnumerable<HttpProxy> proxies);
        List<HttpProxy> Map(string data);
    }
}
