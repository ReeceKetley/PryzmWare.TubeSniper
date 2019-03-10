using System;
using System.Collections.Generic;
using System.Linq;
using TubeSniper.Domain.Accounts;

namespace TubeSniper.Domain.Youtube
{
	public class StandardAccountRegister
	{
		private readonly List<AccountRegisterItem> _items;
		private readonly object _mutex = new object();
		private readonly Random _rand = new Random();

		public StandardAccountRegister(List<AccountEntry> accounts)
		{
			_items = new List<AccountRegisterItem>();
			foreach (var youtubeAccount in accounts)
			{
				_items.Add(new AccountRegisterItem(youtubeAccount));
			}
		}

		public AccountEntry Acquire()
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
					return item.AccountEntry;
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

		public void Release(AccountEntry accountEntry)
		{
			lock (_mutex)
			{
				var items = _items.Where(x => x.AccountEntry == accountEntry);
				foreach (var item in items)
				{
					++item.UseCount;
					item.InUse = false;
				}
			}
		}
	}
}