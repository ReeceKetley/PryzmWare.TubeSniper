using System;
using TubeSniper.Application.Events.Accounts;
using TubeSniper.Domain.Accounts;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces.Persistence;

namespace TubeSniper.Application.Accounts
{
	public class AccountService : IAccountService
	{
		private readonly IAccountEntryRepository _accountEntryRepository;
		private readonly ICampaignService _campaignService;

		public AccountService(IAccountEntryRepository accountEntryRepository, ICampaignService campaignService)
		{
			_accountEntryRepository = accountEntryRepository;
			_campaignService = campaignService;
		}

		public void Delete(Guid id)
		{
			var model = _accountEntryRepository.GetById(id);
			_accountEntryRepository.Delete(model);
			AccountEvents.RaiseAccountProfileRemoved(new AccountProfileRemoved(model));
			_campaignService.HandleAccountDeletion(model);
		}

		public void Insert(AccountEntry accountEntry)
		{
			_accountEntryRepository.Insert(accountEntry);
			var clone = accountEntry.DeepClone();
			AccountEvents.RaiseAccountProfileCreated(new AccountProfileCreated(clone));
		}

		public void Update(AccountEntry accountEntry)
		{
			_accountEntryRepository.Update(accountEntry);
			AccountEvents.RaiseAccountProfileUpdated(new AccountProfileUpdated(accountEntry.DeepClone()));
		}
	}
}