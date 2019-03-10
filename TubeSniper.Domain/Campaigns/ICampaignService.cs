using System;
using System.Collections.Generic;
using TubeSniper.Domain.Accounts;

namespace TubeSniper.Domain.Campaigns
{
	public interface ICampaignService
	{
		void Add(Campaign campaign);
		void Remove(Campaign campaign);
		void Update(Campaign campaign);
		void HandleAccountDeletion(AccountEntry account);
		Campaign GetById(Guid id);
		IEnumerable<Campaign> GetAll();
	}
}