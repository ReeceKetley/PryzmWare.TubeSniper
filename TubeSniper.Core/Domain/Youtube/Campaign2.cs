using System.Collections.Generic;
using System.Net;

namespace TubeSniper.Core.Domain.Youtube
{
    public class Campaign2
    {
        public string SearchTerms { get; }
        public string Comment { get; }
        public List<YoutubeAccount> Accounts { get; }
        public List<WebProxy> Proxies { get; }

        public Campaign2(string searchTerms, string comment, List<YoutubeAccount> accounts, List<WebProxy> proxies)
        {
            SearchTerms = searchTerms;
            Comment = comment;
            Accounts = accounts;
            Proxies = proxies;
        }

        public void SaveCampaign()
        {

        }

        public void ExportCampaign()
        {

        }
    }
}
