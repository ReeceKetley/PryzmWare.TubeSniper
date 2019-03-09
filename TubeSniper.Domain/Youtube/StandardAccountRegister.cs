using System;
using System.Collections.Generic;
using System.Linq;
using TubeSniper.Domain.Models;

namespace TubeSniper.Domain.Youtube
{
    public class StandardAccountRegister : IAccountRegister
    {
        private readonly List<AccountRegisterItem> _items;
		private readonly object _mutex = new object();
		private readonly Random _rand = new Random();

        public StandardAccountRegister(List<YoutubeAccount> accounts)
        {
            _items = new List<AccountRegisterItem>();
	        foreach (var youtubeAccount in accounts)
	        {
		        _items.Add(new AccountRegisterItem(youtubeAccount));
	        }
        }

        public YoutubeAccount Acquire()
        {
	        lock (_mutex)
	        {
		        try
		        {
			        var available = _items.Where(x => !x.InUse).ToList();
			        var lowestCount = available.Min(x => x.UseCount);
			        var lowest = available.Where(x => x.UseCount == lowestCount).ToList();
			        var item = lowest[_rand.Next(lowest.Count)];
			        item.InUse = true;
			        return item.Account;
		        }
		        catch
		        {
			        return null;
		        }
	        }
        }

	    public List<AccountRegisterItem> GetAll()
	    {
		    lock (_mutex)
		    {
			    return _items.ToList();
		    }
	    }

	    public void Release(YoutubeAccount account)
        {
	        lock (_mutex)
	        {
		        var items = _items.Where(x => x.Account == account);
		        foreach (var item in items)
		        {
			        ++item.UseCount;
			        item.InUse = false;
		        }
	        }
        }
	    }
}