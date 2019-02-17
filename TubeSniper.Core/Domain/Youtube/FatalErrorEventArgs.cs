namespace TubeSniper.Core.Domain.Youtube
{
    public class FatalErrorEventArgs
    {
        public string Error { get; }

        public FatalErrorEventArgs(string error)
        {
            Error = error;
        }
    }
}