namespace TubeSniper.Core.Domain.Youtube
{
    public class VideoNaviagorResult
    {
        public VideoNaviagorResultCode Code { get; }

        public VideoNaviagorResult(VideoNaviagorResultCode code)
        {
            Code = code;
        }
    }
}