namespace TubeSniper.Application
{
	public class CommentServiceResult
	{
		public CommentServiceResult(string id, CommentServiceResultArgs code)
		{
			Id = id;
			Code = code;
		}

		public string Id { get; }
		public CommentServiceResultArgs Code { get; }
	}
}