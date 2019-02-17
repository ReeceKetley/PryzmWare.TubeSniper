using System;
using System.IO;
using System.Net;
using System.Threading;
using EO.WebBrowser;
using EO.WebEngine;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Browser
{
	public class VirtualBrowser
	{
		public WebView WebView { get; }
		public VirtualMouse Mouse { get; }
		public VirtualKeyboard Keyboard { get; }
		public WebProxy Proxy { get; }

		protected VirtualBrowser(WebView webView, VirtualMouse mouse, VirtualKeyboard keyboard, WebProxy proxy)
		{
			WebView = webView;
			Mouse = mouse;
			Keyboard = keyboard;
			Proxy = proxy;
		}

		public bool LoadUrlAndVerify(string url)
		{
			if (WebView.ThreadRunner == null)
			{
				WebView.LoadRequestAndWait(new Request(url));
			}
			else
			{
				WebView.Send(() => { WebView.LoadRequestAndWait(new Request(url)); });
			}

			if (WebView.Url != url)
			{
				return false;
			}

			return true;
		}

		public void ImageCapture(string path)
		{
			try
			{
				WebView.Send(() => { WebView.Capture(path); });
			}
			catch
			{
				return;
			}
		}


		public static VirtualBrowser Create(WebProxy proxy, YoutubeAccount youtubeAccount, bool useCache)
		{
			//Runtime.SetDefaultBrowserOptions(new BrowserOptions
			//{
				//EnableWebSecurity = true,
				//AllowPlugins = true,
				//AllowZooming = false,
			//});
			//var engine = CreateEngine(proxy, youtubeAccount, true);
			//var threadRunner = CreateThreadRunner(engine);
			var webView = CreateView(null);

			bool debug = false;

			if (debug)
			{
				Thread t = new Thread(CreateDebug);
				t.Start(webView);
			}
			return new VirtualBrowser(webView, new VirtualMouse(webView), new VirtualKeyboard(webView), proxy);
		}

		private static void CreateDebug(object o)
		{
			var webView = (EO.WebBrowser.WebView) o;
			//DebugView debugView = new DebugView(webView);
			//debugView.Show();
			//CreateEngine()
		}

		private static Engine CreateEngine(WebProxy proxy, YoutubeAccount youtubeAccount, bool useCache)
		{
			var engine = Engine.Create("ts-" + Environment.TickCount);
			var cacheFolder = Environment.TickCount.ToString();
			var proxyString = "null";
			if (youtubeAccount != null)
			{
				if (proxy != null)
				{
					proxyString = GeneralHelpers.MakeValidFileName(proxy.Address.ToString());
				}
				cacheFolder = youtubeAccount.Credentials.Email.Split('@')[0];
			}

			engine.Options.CachePath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) ?? throw new InvalidOperationException(), "cache", cacheFolder);
			if (proxy != null)
			{
				Console.WriteLine("Proxy was set : " + proxy.Address.Host);
				engine.SetWebProxy(proxy);
			}
			return engine;
		}

		private static ThreadRunner CreateThreadRunner(Engine engine)
		{
			var threadRunner = new ThreadRunner("ts-" + Environment.TickCount, engine);
			return threadRunner;
		}

		private static WebView CreateView(ThreadRunner threadRunner)
		{
			//var view = threadRunner.CreateWebView(800, 600);
			var engine = Engine.Create("ts-debug");
			engine.Options.CachePath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath), "debug");
			var view = new WebView();
			view.CustomUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36";
			view.AcceptLanguage = "en-US";
			view.CertificateError += (sender, args1) =>
			{
				args1.Continue();
			};

			//var frm = new DebugView(view);
			//frm.Show();

			return view;
		}
	}
}
