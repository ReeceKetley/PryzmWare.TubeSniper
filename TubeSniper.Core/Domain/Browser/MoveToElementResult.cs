namespace TubeSniper.Core.Domain.Browser
{
    public class MoveToElementResult
    {
        public MoveToElementResultCode Code { get; }

        public MoveToElementResult(MoveToElementResultCode code)
        {
            Code = code;
        }

        public MoveToElementResult()
        {
            Code = MoveToElementResultCode.Success;
        }
    }
}