using System.Net;
using EO.WebBrowser;
using TubeSniper.YouTubeBot.VirtualBrowser;

namespace TubeSniper.YouTubeBot.Model
{
	public interface IVirtualBrowser
	{
		VirtualKeyboard Keyboard { get; }
		VirtualMouse Mouse { get; }
		WebProxy Proxy { get; }
		WebView WebView { get; }

		void ImageCapture(string path);
		bool LoadUrlAndVerify(string url);
	}
}