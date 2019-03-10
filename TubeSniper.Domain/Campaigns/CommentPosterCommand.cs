namespace TubeSniper.Domain.Campaigns
{
	public class CommentPosterCommand
	{
		public CommentPosterCommand()
		{
		}

		public CommentPosterCommand(CommentJob job)
		{
			Job = job;
		}

		public CommentJob Job { get; }

		public static CommentPosterCommand None()
		{
			return new CommentPosterCommand();
		}

		public static CommentPosterCommand Post(CommentJob job)
		{
			return new CommentPosterCommand(job);
		}
	}
}