using Com.CloudRail.SI.Types;

namespace TubeSniper.Core.Domain.Youtube
{
    public class CommentPostedResult
    {
        public VideoMetaData MetaData { get; }
        public string Comment { get; }
        public CommentPostedResultCode Code { get; }

        public CommentPostedResult(VideoMetaData metaData, string comment, CommentPostedResultCode code)
        {
            MetaData = metaData;
            Comment = comment;
            Code = code;
        }
    }
}