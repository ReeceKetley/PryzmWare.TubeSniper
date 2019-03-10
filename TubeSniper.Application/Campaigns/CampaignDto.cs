using System;
using System.Collections.Generic;

namespace TubeSniper.Application.Campaigns
{
	public class CampaignDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Keyword { get; set; }
		public List<Guid> Accounts { get; set; } = new List<Guid>();
		public List<Guid> Proxies { get; set; } = new List<Guid>();
		public string Comment { get; set; }
		public string CommentMethod { get; set; }
		public int MaxComments { get; set; }
		public int NumberOfWorkers { get; set; }
		public List<string> ProcessedVideos { get; set; } = new List<string>();
	}
}