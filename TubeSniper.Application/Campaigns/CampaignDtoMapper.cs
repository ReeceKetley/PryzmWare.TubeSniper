using System.Collections.Generic;
using TubeSniper.Application.Accounts;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Services;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Campaigns
{
	public class CampaignMapper : ICampaignMapper
	{
		private readonly ICaptchaService _captchaService;
		private readonly IProxyTestService _proxyTestService;
		private readonly IYoutubeCommentBotFactory _botFactory;
		private readonly ISearchService _searchService;

		public CampaignMapper(ISearchService searchService, ICaptchaService captchaService, IProxyTestService proxyTestService, IYoutubeCommentBotFactory botFactory)
		{
			_searchService = searchService;
			_captchaService = captchaService;
			_proxyTestService = proxyTestService;
			_botFactory = botFactory;
		}

		public CampaignDto Map(Campaign campaign)
		{
			var dto = new CampaignDto();
			dto.Id = campaign.Id;
			var accounts = new List<AccountDto>();
			foreach (var campaignMetaAccount in campaign.Meta.Accounts)
			{
				accounts.Add(campaignMetaAccount.ToDto());
			}

			dto.Accounts = accounts;
			dto.Comment = campaign.Comment;
			dto.Name = campaign.Meta.Title;
			dto.ProcessedIds = campaign.Meta.VideoProccesed;
			dto.Keyword = campaign.Meta.SearchTerm;
			dto.Proxies = campaign.ProxyCollection.Proxies;
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

			meta.Accounts = accounts;
			meta.VideoProccesed = campaignDto.ProcessedIds;
			var campaign = new Campaign(new ProxyCollection(campaignDto.Proxies), meta, new StandardAccountRegister(accounts), campaignDto.Keyword, new CommentGenerator(new CommentTemplate(campaignDto.Comment)), campaignDto.AsReply, _searchService, _captchaService, _proxyTestService, _botFactory);
			campaign.Id = campaignDto.Id;
			return campaign;
		}
	}
}