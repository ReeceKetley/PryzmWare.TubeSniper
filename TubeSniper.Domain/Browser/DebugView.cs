using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Browser
{
	public partial class DebugView : Form
	{
		private YoutubeBot bot;
		public DebugView()
		{
			InitializeComponent();
			var proxy = new WebProxy();
			//bot = new YoutubeBot(new YoutubeAccount(new YoutubeCredentials("jacobsholmes925@gmail.com", "PryzmWare!1234"), "jacobsholmes315@gmail.com"), new HttpProxy(new HttpProxyAddress(proxy.Address.Host)), new YoutubeVideo("1S0aBV-Waeo", "", "", ""), "very intresting video!", true, false);
			WebControl.WebView = bot.Browser.WebView;
			//WebControl.WebView.Engine.Options.CachePath = Guid.NewGuid().ToString();
			//WebControl.WebView.Engine.Options.Proxy = new ProxyInfo(ProxyType.HTTP, "1.17.154.4", 8080);
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

		private void button1_Click(object sender, EventArgs e)
		{
			var source = WebControl.WebView.GetHtml();
			var fileName = "source\\" + GeneralHelpers.MakeValidFileName(WebControl.WebView.Url.Substring(WebControl.WebView.Url.LastIndexOf('/')) + ".html");
		}
	}
}
