namespace TubeSniper.YouTubeBot.Youtube
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