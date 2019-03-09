using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Models
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
