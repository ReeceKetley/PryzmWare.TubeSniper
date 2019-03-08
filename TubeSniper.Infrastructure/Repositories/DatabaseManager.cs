using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LiteDB;
using TubeSniper.Core;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Infrastructure.Models;

namespace TubeSniper.Infrastructure.Repositories
{
    public static class DatabaseManager
    {
        private static LiteDatabase _database = new LiteDatabase(@"data\core.dat");

	    public static ApplicationSettings GetSettings()
	    {
		    var settingsQuery = _database.GetCollection<ApplicationSettings>("settings");
		    var settings = settingsQuery.FindAll();
		    return settings.GetEnumerator().Current;
	    }

		public static void SaveSettings(ApplicationSettings setting)
	    {
		    var settingsQuery = _database.GetCollection<ApplicationSettings>("settings");
			settingsQuery.Insert("default", setting);
		}

        public static List<YoutubeAccount> GetAccounts()
        {
            var accounts  = _database.GetCollection<YoutubeDatabaseAccountEntry>("accounts");
            var accountsList = new List<YoutubeAccount>();
            foreach (var account in accounts.FindAll())
            {
                accountsList.Add(new YoutubeAccount(new YoutubeCredentials(account.Email, account.Password), account.Recovery));
            }
            return accountsList;
        }

	    public static List<Campaign> GetCampaigns()
	    {
		    var campagins = _database.GetCollection<Campaign>("campaigns");
		    return campagins.FindAll().ToList();
	    }

	    public static bool AddCampaign(List<Campaign> campaigns)
	    {
		    var campaginsCollection = _database.GetCollection<Campaign>("campaigns");
		    foreach (var campaign in campaigns)
		    {
			    campaginsCollection.Insert(Environment.TickCount, campaign);
		    }
		    return true;
	    }

        public static bool AddAccount(YoutubeAccount account)
        {
            var accounts = _database.GetCollection<YoutubeDatabaseAccountEntry>("accounts");
            var dbAccountEntry = new YoutubeDatabaseAccountEntry();
            dbAccountEntry.Email = account.Credentials.Email;
            dbAccountEntry.Password = account.Credentials.Password;
            dbAccountEntry.Recovery = account.RecoveryEmail;
            accounts.Insert(Environment.TickCount, dbAccountEntry);
            return true;
        }

        public static bool AddProxy(WebProxy proxy)
        {
            var proxies = _database.GetCollection<WebProxy>("proxies");
            proxies.Insert(Environment.TickCount, proxy);
            return true;
        }

        public static List<WebProxy> GetProxies()
        {
            var proxies = _database.GetCollection<WebProxy>("proxies");
            foreach (var proxy in proxies.FindAll())
            {
                //Console.WriteLine(proxy.Address);
                //Console.WriteLine();
            }
            return new List<WebProxy>();
        }


        public static bool AddComment(CommentTemplate comment)
        {
            var comments = _database.GetCollection<CommentTemplate>("comments");
            comments.Insert(Environment.TickCount, comment);
            return true;
        }

        public static List<CommentTemplate> GetComments()
        {
            var comments = _database.GetCollection<CommentTemplate>("comment");
            foreach (var comment in comments.FindAll())
            {
                //Console.WriteLine(comment.ToString());
                //Console.WriteLine();
            }
            return new List<CommentTemplate>();
        }

    }
}
