using System.Drawing;

namespace TubeSniper.YouTubeBot.Youtube
{
    internal class ExtractCatpchaResult
    {
        public ExtractCatpchaResultCode Code { get; }
        public Image Image { get; }

        public ExtractCatpchaResult(ExtractCatpchaResultCode code)
        {
            Code = code;
        }

        public ExtractCatpchaResult(Image image)
        {
            Image = image;
            Code = ExtractCatpchaResultCode.Successs;
        }
    }
}