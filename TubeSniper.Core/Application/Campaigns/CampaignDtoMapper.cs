using System.Collections.Generic;
using TubeSniper.Core.Application.Accounts;
using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Domain.Proxies;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Interfaces;

namespace TubeSniper.Core.Application.Campaigns
{
	public class CampaignMappper : ICampaignMappper
	{
		private readonly ISearchService _searchService;

		public CampaignMappper(ISearchService searchService)
		{
			_searchService = searchService;
		}

		public CampaignDto Map(Campaign campaign)
		{
			var dto = new CampaignDto();
			dto.Id = campaign.Id;
			var accounts = new List<AccountDto>();
			foreach (var campaignMetaAccount in campaign.CampaignMeta.Accounts)
			{
				accounts.Add(campaignMetaAccount.ToDto());
			}

			dto.Accounts = accounts;
			dto.Comment = campaign.BaseComment;
			dto.VaribablesDictionary = campaign.Variables;
			dto.Name = campaign.CampaignMeta.Title;
			dto.ProcessedIds = campaign.CampaignMeta.VideoProccesed;
			dto.Keyword = campaign.CampaignMeta.SearchTerm;
			dto.Proxies = campaign.CampaignMeta.ProxyRegister.Proxies;
			dto.AsReply = campaign.AsReply;
			return dto;
		}

		public Campaign Map(CampaignDto campaignDto)
		{
			var meta = new CampaignMeta();
			meta.Title = campaignDto.Name;
			meta.SearchTerm = campaignDto.Keyword;
			var accounts = new List<YoutubeAccount>();
			if (campaignDto.Accounts == null)
			{
				return null;
			}

			foreach (var campaignMetaAccount in campaignDto.Accounts)
			{
				accounts.Add(campaignMetaAccount.FromDto());
			}

			meta.ProxyRegister = new ProxyRegister(campaignDto.Proxies);
			meta.Accounts = accounts;
			meta.VideoProccesed = campaignDto.ProcessedIds;
			var campaign = new Campaign(meta, new StandardAccountRegister(accounts), campaignDto.Keyword, campaignDto.Comment, campaignDto.VaribablesDictionary, campaignDto.AsReply, _searchService);
			campaign.Id = campaignDto.Id;
			return campaign;
		}
	}
}