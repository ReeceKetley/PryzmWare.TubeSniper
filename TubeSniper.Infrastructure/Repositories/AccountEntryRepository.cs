using System;
using System.Collections.Generic;
using LiteDB;
using TubeSniper.Application.Accounts;
using TubeSniper.Domain.Accounts;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Infrastructure.Repositories
{
	public class AccountEntryRepository : IAccountEntryRepository
	{
		private readonly IAccountEntryMapper _accountEntryMapper;
		private readonly LiteDatabase _database = new LiteDatabase(@"data\core.dat");

		public AccountEntryRepository(IAccountEntryMapper accountEntryMapper)
		{
			_accountEntryMapper = accountEntryMapper;
		}

		public AccountEntry GetById(Guid id)
		{
			var collection = _database.GetCollection<AccountEntryDto>("accounts");
			var accountDto = collection.FindOne(x => x.Id == id);
			return _accountEntryMapper.Map(accountDto);
		}

		public AccountEntry GetByIdOrDefault(Guid id)
		{
			var collection = _database.GetCollection<AccountEntryDto>("accounts");
			var campaignDto = collection.FindOne(x => x.Id == id);
			if (campaignDto == null)
			{
				return null;
			}
			return _accountEntryMapper.Map(campaignDto);
		}

		public IEnumerable<AccountEntry> GetAll()
		{
			var collection = _database.GetCollection<AccountEntryDto>("accounts");
			var models = new List<AccountEntry>();
			foreach (var dto in collection.FindAll())
			{
				models.Add(_accountEntryMapper.Map(dto));
			}

			return models;
		}

		public void Insert(AccountEntry model)
		{
			if (model.Id == Guid.Empty)
			{
				model.Id = Guid.NewGuid();
			}

			var accountDto = _accountEntryMapper.Map(model);
			var collection = _database.GetCollection<AccountEntryDto>("accounts");
			collection.Insert(accountDto);
		}

		public void Delete(AccountEntry model)
		{
			var accountDto = _accountEntryMapper.Map(model);
			var collection = _database.GetCollection<AccountEntryDto>("accounts");
			collection.Delete(accountDto.Id);
		}

		public void Update(AccountEntry model)
		{
			var collection = _database.GetCollection<AccountEntryDto>("accounts");
			collection.Update(_accountEntryMapper.Map(model));
		}
	}
}
