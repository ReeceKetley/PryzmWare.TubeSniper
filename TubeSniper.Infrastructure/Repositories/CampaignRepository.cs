using System;
using System.Collections.Generic;
using LiteDB;
using TubeSniper.Core.Application.Campaigns;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Interfaces.Persistence;

namespace TubeSniper.Infrastructure.Repositories
{
	public class CampaignRepository : ICampaignRepository
	{
		private readonly ICampaignMappper _campaignMappper;
		private LiteDatabase _database = new LiteDatabase(@"data\core.dat");

		public CampaignRepository(ICampaignMappper campaignMappper)
		{
			_campaignMappper = campaignMappper;
		}

		public void Insert(Campaign campaign)
		{
			if (campaign.Id == Guid.Empty)
			{
				campaign.Id = Guid.NewGuid();
			}

			var campaignDto = _campaignMappper.Map(campaign);
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			collection.Insert(campaignDto);
		}

		public void Delete(Campaign campaign)
		{
			var campaignDto = _campaignMappper.Map(campaign);
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			collection.Delete(campaignDto.Id);
		}

		public void Update(Campaign campaign)
		{
			var campaignDto = _campaignMappper.Map(campaign);
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			collection.Update(campaignDto);
		}

		public IEnumerable<Campaign> GetAll()
		{
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			var list = new List<Campaign>();
			foreach (var campaignDto in collection.FindAll())
			{
				var campaign = _campaignMappper.Map(campaignDto);
				list.Add(campaign);
			}

			return list;
		}

		public Campaign GetById(Guid id)
		{
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			var campaignDto = collection.FindOne(x => x.Id == id);
			return _campaignMappper.Map(campaignDto);
		}

		public Campaign GetByIdOrDefault(Guid id)
		{
			var collection = _database.GetCollection<CampaignDto>("campaigns");
			var campaignDto = collection.FindOne(x => x.Id == id);
			return _campaignMappper.Map(campaignDto);
		}
	}
}