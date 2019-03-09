using System;
using System.Collections.Generic;
using TubeSniper.Application.Events.Campaigns;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Campaigns
{
	public class CampaignService : ICampaignService
	{
		private readonly ICampaignMapper _campaignMapper;
		private readonly IYoutubeCommentBot _commentBot;
		private readonly ICampaignRepository _campaignRepository;

		public CampaignService(ICampaignRepository campaignRepository, ICampaignMapper campaignMapper, IYoutubeCommentBot commentBot)
		{
			_campaignRepository = campaignRepository;
			_campaignMapper = campaignMapper;
			_commentBot = commentBot;
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
			var dto = _campaignMapper.Map(campaign);
			var copy = _campaignMapper.Map(dto);
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

			var dto = _campaignMapper.Map(campaign);
			var copy = _campaignMapper.Map(dto);
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


			var dto = _campaignMapper.Map(campaign);
			var copy = _campaignMapper.Map(dto);
			_campaignRepository.Update(campaign);
			CampaignEvents.RaiseCampaignUpdated(new CampaignUpdated(copy));
		}

		public void Start(Campaign campaign)
		{

		}

		public Campaign GetById(Guid id)
		{
			var campaign = _campaignRepository.GetByIdOrDefault(id);
			if (campaign == null)
			{
				return null;
			}

			var dto = _campaignMapper.Map(campaign);
			var copy = _campaignMapper.Map(dto);
			return copy;
		}

		public IEnumerable<Campaign> GetAll()
		{
			return _campaignRepository.GetAll();
		}
	}
}