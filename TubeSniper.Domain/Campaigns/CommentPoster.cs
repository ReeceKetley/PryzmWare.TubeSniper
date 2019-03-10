namespace TubeSniper.Domain.Campaigns
{
	public class CommentPoster
	{
		private readonly Campaign _campaign;

		public CommentPoster(Campaign campaign)
		{
			_campaign = campaign;
		}

		public CommentPosterCommand Next()
		{
			if (!_campaign.IsRunning)
			{
				return CommentPosterCommand.None();
			}

			return CommentPosterCommand.Post(_campaign.NextJob());
		}

		public void Process(CommentPostedEvent commentPostedEvent)
		{
			_campaign.Process(commentPostedEvent);
		}
	}
}