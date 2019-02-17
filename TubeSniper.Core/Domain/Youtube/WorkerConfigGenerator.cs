using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace TubeSniper.Core.Domain.Youtube
{
    public class WorkerConfigGenerator
    {
        public List<YoutubeAccount> Accounts { get; }
        public List<WebProxy> Proxies { get; }
        public List<string> SearchTerms { get; }
        public List<string> Comments { get; }

        public WorkerConfigGenerator(List<YoutubeAccount> accounts, List<WebProxy> proxies, List<string> searchTerms,List<string> comments)
        {
            Accounts = accounts;
            Proxies = proxies;
            SearchTerms = searchTerms;
            Comments = comments;
        }

        public JObject ToJObject()
        {
            var configObject = new JObject();
            var accountsObject = new JObject();
            foreach (var account in Accounts)
            {
                accountsObject.Add(account);
            }
            configObject.Add("accounts", accountsObject);

            var proxiesObject = new JObject();
            foreach (var webProxy in Proxies)
            {
                proxiesObject.Add(webProxy.Address);
            }
            configObject.Add("proxies", proxiesObject);

            var searchTermsObject = new JObject();
            foreach (var searchTerm in SearchTerms)
            {
                searchTermsObject.Add(searchTerm);
            }
            configObject.Add("searchTerms", searchTermsObject);

            var commentsObject = new JObject();
            foreach (var comment in Comments)
            {
                commentsObject.Add(comment);
            }
            configObject.Add("comments", commentsObject);

            return configObject;
        }
    }
}
