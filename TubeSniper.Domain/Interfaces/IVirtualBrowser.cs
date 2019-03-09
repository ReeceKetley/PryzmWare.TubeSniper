using System.Net;
using EO.WebBrowser;
using TubeSniper.Domain.Youtube.VirtualBrowser;

namespace TubeSniper.Domain.Interfaces
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