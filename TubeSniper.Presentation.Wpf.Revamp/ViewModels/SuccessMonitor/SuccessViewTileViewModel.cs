using TubeSniper.Core.Domain.Campaigns;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Presentation.Wpf.ViewModels.SuccessMonitor
{
	public class SuccessViewTileViewModel : ViewModelBase
	{
		public string VideoTitle { get; set; }
		public string ImageSource { get; set; }
		public string CampaignTitle { get; set; }
		public string Url { get; set; }
		public string Comment { get; set; }
		public string SearchTerm { get; set; }

		public void SetVideo(YoutubeVideo video, CampaignMeta campaignMeta, string comment)
		{
			CampaignTitle = video.Title;
			Url = "https://www.youtube.com/watch?v=" + video.Id;
			Comment = comment;
			VideoTitle = video.Title;
			ImageSource = video.ThumbnailUrl;
			SearchTerm = campaignMeta.SearchTerm;
		}
	}
}
