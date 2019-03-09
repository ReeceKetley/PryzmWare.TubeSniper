using System;
using System.Collections.Generic;
using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Domain.Interfaces.Persistence
{
	public interface ICampaignRepository
	{
		void Insert(Campaign campaign);
		void Delete(Campaign campaign);
		void Update(Campaign campaign);
		IEnumerable<Campaign> GetAll();
		Campaign GetById(Guid id);
		Campaign GetByIdOrDefault(Guid id);
	}
}
