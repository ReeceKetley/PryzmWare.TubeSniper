namespace TubeSniper.Presentation.Wpf.Services
{
    public interface IClipboardService
    {
        bool SetText(string text);
        string GetText();
    }
}
