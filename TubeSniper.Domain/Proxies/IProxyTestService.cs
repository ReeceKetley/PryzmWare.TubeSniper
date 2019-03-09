namespace TubeSniper.Domain.Proxies
{
	public interface IProxyTestService
	{
		ProxyTestResult TestProxy(HttpProxy proxy);
	}
}