namespace TubeSniper.Application.Services
{
	public interface IClipboardService
	{
		bool SetText(string text);
		string GetText();
	}
}