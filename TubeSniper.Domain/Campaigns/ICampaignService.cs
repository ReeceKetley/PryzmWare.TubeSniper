using System;
using System.Collections.Generic;

namespace TubeSniper.Domain.Campaigns
{
	public interface ICampaignService
	{
		void Add(Campaign campaign);
		void Remove(Campaign campaign);
		void Update(Campaign campaign);
		Campaign GetById(Guid id);
		IEnumerable<Campaign> GetAll();
	}
}