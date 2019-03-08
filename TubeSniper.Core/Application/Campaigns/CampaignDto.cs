﻿using System;
using System.Collections.Generic;
using TubeSniper.Core.Application.Accounts;
using TubeSniper.Core.Domain.Proxies;

namespace TubeSniper.Core.Application.Campaigns
{
	public class CampaignDto
	{
		public List<AccountDto> Accounts { get; set; }
		public bool AsReply { get; set; }
		public string Comment { get; set; }
		public Guid Id { get; set; }
		public string Keyword { get; set; }
		public string Name { get; set; }
		public List<string> ProcessedIds { get; set; }
		public List<ProxyEntry> Proxies { get; set; }
		public Dictionary<string, string> VaribablesDictionary { get; set; }
	}
}