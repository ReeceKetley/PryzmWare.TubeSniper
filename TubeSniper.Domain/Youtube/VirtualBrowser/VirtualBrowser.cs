using System;
using System.IO;
using System.Net;
using EO.WebBrowser;
using EO.WebEngine;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Youtube.Extensions;

namespace TubeSniper.Domain.Youtube.VirtualBrowser
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
				// do nothing
			}
		}


		public static VirtualBrowser Create(WebProxy proxy, YoutubeAccount youtubeAccount, bool useCache)
		{
			Runtime.SetDefaultBrowserOptions(new BrowserOptions
			{
				EnableWebSecurity = true,
				AllowPlugins = true,
				AllowZooming = false,
			});
			var engine = CreateEngine(proxy, youtubeAccount);
			var threadRunner = CreateThreadRunner(engine);
			var webView = CreateView(threadRunner); // use null for debug
			return new VirtualBrowser(webView, new VirtualMouse(webView), new VirtualKeyboard(webView), proxy);
		}


		private static Engine CreateEngine(WebProxy proxy, YoutubeAccount youtubeAccount)
		{
			var currentDirectory = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
			var cachePath = Path.Combine(currentDirectory ?? throw new InvalidOperationException(), "cache", GeneralHelpers.MakeValidFileName(youtubeAccount.Credentials.Email));
			// TODO: Hash username
			if (proxy != null)
			{
				cachePath = Path.Combine(cachePath, GeneralHelpers.MakeValidFileName(proxy.Address.ToString()));
			}

			var engine = Engine.Create("ts-" + Environment.TickCount);
			engine.Options.CachePath = cachePath;
			if (proxy != null)
			{
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
			var view = threadRunner.CreateWebView(320, 568);
			var engine = Engine.Create("ts-debug");
			engine.Options.CachePath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) ?? throw new InvalidOperationException(), "debug");
			//var view = new WebView();
			view.CustomUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36";
			//Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3
			view.AcceptLanguage = "en-US";
			view.CertificateError += (sender, args1) =>
			{
				args1.Continue();
			};

			return view;
		}

		public void Reset()
		{
			WebView.Engine.Stop(false);
			WebView.Destroy();
		}
	}
}
