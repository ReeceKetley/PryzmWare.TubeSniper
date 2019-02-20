using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using EO.Base;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Browser
{
	public partial class DebugView : Form
	{
		private YoutubeBot bot;
		public DebugView()
		{
			InitializeComponent();
			var proxy = new WebProxy();
			bot = new YoutubeBot(new YoutubeAccount(new YoutubeCredentials("jacobsholmes925@gmail.com", "PryzmWare!1234"), "jacobsholmes315@gmail.com"), proxy, new YoutubeVideo("O1nok2VlF1M", "", "", ""), "epic man", false, false);
			WebControl.WebView = bot._browser.Browser.WebView;
			WebControl.WebView.Engine.Options.CachePath = Guid.NewGuid().ToString();
			WebControl.WebView.Engine.Options.Proxy = new ProxyInfo(ProxyType.HTTP, "83.151.4.172", 57812);
			WebControl.WebView.UrlChanged += WebView_UrlChanged;
		}

		private void WebView_UrlChanged(object sender, EventArgs e)
		{
			textBox1.Text = WebControl.WebView.Url;
			WebControl.WebView.ShowDevTools(panel1.Handle);

		}


		private void Start()
		{
			bot.Run();
		}

		private void DebugView_Load(object sender, EventArgs e)
		{
			//WebControl.WebView.LoadHtmlAndWait("https://google.com");
			//WebControl.WebView.LoadUrl("https://recaptcha-demo.appspot.com/recaptcha-v3-request-scores.php");
			Thread t = new Thread(Start);
			t.Start();
		}

		private void WebControl_Click(object sender, EventArgs e)
		{

		}
	}
}
