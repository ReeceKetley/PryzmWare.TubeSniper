namespace TubeSniper.Domain.Youtube
{
    public class PostCommentResult
    {
        public string Id { get; }
        public PostCommentResultCode Code { get; }

        public PostCommentResult(PostCommentResultCode code)
        {
            Code = code;
        }

        public PostCommentResult(string id)
        {
            Id = id;
            Code = PostCommentResultCode.Success;
        }
    }
}