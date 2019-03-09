using System;
using System.Collections.Generic;
using LiteDB;
using TubeSniper.Application.Accounts;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Infrastructure.Repositories
{
	public class AccountsRepository : IAccountsRepository
	{
		private readonly LiteDatabase _database = new LiteDatabase(@"data\core.dat");

		public void Insert(YoutubeAccount account)
		{
			if (account.Id == Guid.Empty)
			{
				account.Id = Guid.NewGuid();
			}

			var accountDto = account.ToDto();
			var collection = _database.GetCollection<AccountDto>("accounts");
			collection.Insert(accountDto);
		}

		public void Delete(YoutubeAccount account)
		{
			var accountDto = account.ToDto();
			var collection = _database.GetCollection<AccountDto>("accounts");
			collection.Delete(accountDto.Id);
		}

		public void Update(YoutubeAccount account)
		{
			var accountDto = account.ToDto();
			var collection = _database.GetCollection<AccountDto>("accounts");
			collection.Update(accountDto);
		}

		public IEnumerable<YoutubeAccount> GetAll()
		{
			var collection = _database.GetCollection<AccountDto>("accounts");
			var list = new List<YoutubeAccount>();
			foreach (var accountDto in collection.FindAll())
			{
				list.Add(accountDto.FromDto());
			}

			return list;
		}

		public YoutubeAccount GetById(Guid id)
		{
			var collection = _database.GetCollection<AccountDto>("accounts");
			var accountDto = collection.FindOne(x => x.Id == id);
			return accountDto.FromDto();
		}

		public YoutubeAccount GetByIdOrDefault(Guid id)
		{
			var collection = _database.GetCollection<AccountDto>("accounts");
			var campaignDto = collection.FindOne(x => x.Id == id);
			return campaignDto.FromDto();
		}
	}
}