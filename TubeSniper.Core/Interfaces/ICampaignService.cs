using System;
using System.Collections.Generic;
using TubeSniper.Core.Domain.Campaigns;

namespace TubeSniper.Core.Interfaces
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