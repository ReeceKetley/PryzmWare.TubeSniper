using System.Collections.Generic;
using TubeSniper.Domain.Accounts;
using TubeSniper.Domain.Campaigns;
using TubeSniper.Domain.Interfaces.Persistence;
using TubeSniper.Domain.Proxies;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Campaigns
{
	public class CampaignMapper : ICampaignMapper
	{
		private readonly IAccountEntryRepository _accountEntryRepository;
		private readonly IProxyEntryRepository _proxyEntryRepository;

		public CampaignMapper(IAccountEntryRepository accountEntryRepository, IProxyEntryRepository proxyEntryRepository)
		{
			_accountEntryRepository = accountEntryRepository;
			_proxyEntryRepository = proxyEntryRepository;
		}

		public Campaign Map(CampaignDto dto)
		{
			var model = new Campaign();
			model.Id = dto.Id;

			if (!string.IsNullOrEmpty(dto.Title))
			{
				if (!CampaignTitle.TryCreate(dto.Title, out var title))
				{
					return null;
				}

				model.Title = title;
			}

			if (!string.IsNullOrEmpty(dto.Keyword))
			{
				if (!Keyword.TryCreate(dto.Keyword, out var keyword))
				{
					return null;
				}

				model.Keyword = keyword;
			}

			var accounts = new List<AccountEntry>();
			foreach (var accountId in dto.Accounts)
			{
				var accountEntry = _accountEntryRepository.GetByIdOrDefault(accountId);
				if (accountEntry != null)
				{
					accounts.Add(accountEntry);
				}
			}

			model.Accounts = accounts;

			var proxies = new List<ProxyEntry>();
			foreach (var proxyId in dto.Proxies)
			{
				var proxyEntry = _proxyEntryRepository.GetByIdOrDefault(proxyId);
				if (proxyEntry != null)
				{
					proxies.Add(proxyEntry);
				}
			}

			model.Proxies = proxies;

			if (!string.IsNullOrEmpty(dto.Comment))
			{
				if (!Comment.TryCreate(dto.Comment, out var comment))
				{
					return null;
				}

				model.Comment = comment;
			}

			if (!string.IsNullOrEmpty(dto.CommentMethod))
			{
				if (!CommentMethod.TryParse(dto.CommentMethod, out var commentMethod))
				{
					return null;
				}

				model.CommentMethod = commentMethod;
			}

			model.MaxComments = dto.MaxComments;
			model.NumberOfWorkers = dto.NumberOfWorkers;

			var processedVideos = new List<VideoId>();
			foreach (var videoIdValue in dto.ProcessedVideos)
			{
				if (!VideoId.TryCreate(videoIdValue, out var videoId))
				{
					continue;
				}

				processedVideos.Add(videoId);
			}

			model.ProcessedVideos = processedVideos;

			if (!model.CheckIsValid())
			{
				return null;
			}

			return model;
		}

		public CampaignDto Map(Campaign model)
		{
			var dto = new CampaignDto();
			dto.Id = model.Id;
			dto.Title = model.Title?.Value;
			dto.Keyword = model.Keyword?.Value;
			if (model.Accounts != null)
			{
				foreach (var account in model.Accounts)
				{
					dto.Accounts.Add(account.Id);
				}
			}

			if (model.Proxies != null)
			{
				foreach (var proxy in model.Proxies)
				{
					dto.Proxies.Add(proxy.Id);
				}
			}

			dto.Comment = model.Comment?.Value;
			dto.CommentMethod = model.CommentMethod?.Value;
			dto.MaxComments = model.MaxComments;
			dto.NumberOfWorkers = model.NumberOfWorkers;

			if (model.ProcessedVideos != null)
			{
				foreach (var videoId in model.ProcessedVideos)
				{
					dto.ProcessedVideos.Add(videoId.Value);
				}
			}

			return dto;
		}
	}
}