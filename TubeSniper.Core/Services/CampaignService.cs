using System;
using System.Collections.Generic;
using TubeSniper.Core.Application.Campaigns;
using TubeSniper.Core.Application.Events.Campaigns;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Interfaces;
using TubeSniper.Core.Interfaces.Persistence;

namespace TubeSniper.Core.Services
{
	public class CampaignService : ICampaignService
	{
		private readonly ICampaignRepository _campaignRepository;
		private readonly ICampaignMappper _campaignMappper;

		public CampaignService(ICampaignRepository campaignRepository, ICampaignMappper campaignMappper)
		{
			_campaignRepository = campaignRepository;
			_campaignMappper = campaignMappper;
		}

		public void Add(Campaign campaign)
		{
			if (campaign == null)
			{
				throw new ArgumentNullException(nameof(campaign));
			}

			if (campaign.Id != Guid.Empty)
			{
				throw new Exception();
			}

			_campaignRepository.Insert(campaign);
			var dto = _campaignMappper.Map(campaign);
			var copy = _campaignMappper.Map(dto);
			CampaignEvents.RaiseCampaignCreated(new CampaignCreated(copy));
		}

		public void Remove(Campaign campaign)
		{
			if (campaign == null)
			{
				throw new ArgumentNullException(nameof(campaign));
			}

			if (campaign.Id == Guid.Empty)
			{
				throw new Exception();
			}

			var dto = _campaignMappper.Map(campaign);
			var copy = _campaignMappper.Map(dto);
			_campaignRepository.Delete(campaign);
			CampaignEvents.RaiseCampaignRemoved(new CampaignRemoved(copy));
		}

		public void Update(Campaign campaign)
		{
			if (campaign == null)
			{
				throw new ArgumentNullException(nameof(campaign));
			}

			if (campaign.Id == Guid.Empty)
			{
				throw new Exception();
			}

			if (_campaignRepository.GetByIdOrDefault(campaign.Id) == null)
			{
				throw new Exception();
			}


			var dto = _campaignMappper.Map(campaign);
			var copy = _campaignMappper.Map(dto);
			_campaignRepository.Update(campaign);
			CampaignEvents.RaiseCampaignUpdated(new CampaignUpdated(copy));
		}

		public Campaign GetById(Guid id)
		{
			var campaign = _campaignRepository.GetByIdOrDefault(id);
			if (campaign == null)
			{
				return null;
			}

			var dto = _campaignMappper.Map(campaign);
			var copy = _campaignMappper.Map(dto);
			return copy;
		}

		public IEnumerable<Campaign> GetAll()
		{
			return _campaignRepository.GetAll();
		}
	}
}
