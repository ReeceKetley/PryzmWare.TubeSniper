using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models
{
    public class ProxyAccountPair
    {
        public YoutubeAccount Account { get; }
        public ProxyEntry Proxy { get; }

        public ProxyAccountPair(YoutubeAccount account, ProxyEntry proxy)
        {
            Account = account;
            Proxy = proxy;
        }
    }
}
