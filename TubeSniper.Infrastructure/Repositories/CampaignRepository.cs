using System;
using System.Collections.Generic;
using LiteDB;
using TubeSniper.Application.Campaigns;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces.Persistence;

namespace TubeSniper.Infrastructure.Repositories
{
	public class CampaignRepository : ICampaignRepository
	{
		private readonly ICampaignMapper _campaignMapper;
		private readonly LiteDatabase _database = new LiteDatabase(@"data\core.dat");

		public CampaignRepository(ICampaignMapper campaignMapper)
		{
			_campaignMapper = campaignMapper;
		}

		public void Insert(Campaign campaign)
		{
			if (campaign.Id == Guid.Empty)
			{
				campaign.Id = Guid.NewGuid();
			}

			var campaignDto = _campaignMapper.Map(campaign);
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			collection.Insert(campaignDto);
		}

		public void Delete(Campaign campaign)
		{
			var campaignDto = _campaignMapper.Map(campaign);
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			collection.Delete(campaignDto.Id);
		}

		public void Update(Campaign campaign)
		{
			var campaignDto = _campaignMapper.Map(campaign);
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			collection.Update(campaignDto);
		}

		public IEnumerable<Campaign> GetAll()
		{
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			var list = new List<Campaign>();
			foreach (var campaignDto in collection.FindAll())
			{
				var campaign = _campaignMapper.Map(campaignDto);
				list.Add(campaign);
			}

			return list;
		}

		public Campaign GetById(Guid id)
		{
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			var campaignDto = collection.FindOne(x => x.Id == id);
			return _campaignMapper.Map(campaignDto);
		}

		public Campaign GetByIdOrDefault(Guid id)
		{
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			var campaignDto = collection.FindOne(x => x.Id == id);
			return _campaignMapper.Map(campaignDto);
		}
	}
}