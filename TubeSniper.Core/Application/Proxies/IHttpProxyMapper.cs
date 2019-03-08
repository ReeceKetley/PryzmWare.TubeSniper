using System.Collections.Generic;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Proxies
{
    public interface IHttpProxyMapper
    {
        HttpProxy Map(HttpProxyDto dto);
        HttpProxyDto Map(HttpProxy model);
        List<HttpProxy> Map(IEnumerable<HttpProxyDto> dtos);
        List<HttpProxyDto> Map(IEnumerable<HttpProxy> models);
    }
}
