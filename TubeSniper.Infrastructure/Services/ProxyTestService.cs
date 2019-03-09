using System;
using System.Diagnostics;
using System.Net;
using TubeSniper.Domain.Proxies;

namespace TubeSniper.Infrastructure.Services
{
	public class ProxyTestService : IProxyTestService
	{
		public ProxyTestResult TestProxy(HttpProxy proxy)
		{
			var stopWatch = new Stopwatch();
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://youtube.com");
			request.Proxy = proxy.ToWebProxy();
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
			request.Timeout = 30000;
			stopWatch.Start();
			try
			{
				WebResponse response = request.GetResponse();
			}
			catch (Exception)
			{
				stopWatch.Stop();
				return new ProxyTestResult(false);
			}
			stopWatch.Stop();
			return new ProxyTestResult(true, (int)stopWatch.ElapsedMilliseconds);
		}
	}
}
