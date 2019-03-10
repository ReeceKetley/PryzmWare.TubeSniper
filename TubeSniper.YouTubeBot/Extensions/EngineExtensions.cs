using System;
using System.Net;
using EO.Base;
using EO.WebEngine;

namespace TubeSniper.YouTubeBot.Extensions
{
    internal static class EngineExtensions
    {
        public static void SetWebProxy(this Engine engine, WebProxy proxy)
        {
            var credentials = proxy.Credentials as NetworkCredential;
            string username = null;
            string password = null;
            if (credentials != null)
            {
                username = credentials.UserName;
                password = credentials.Password;
            }
            var proxyInfo = new ProxyInfo(proxy.Address.Scheme == Uri.UriSchemeHttp ? ProxyType.HTTP : ProxyType.HTTPS, proxy.Address.Host, proxy.Address.Port, username,
                password);

            engine.Options.Proxy = proxyInfo;
        }
    }
}