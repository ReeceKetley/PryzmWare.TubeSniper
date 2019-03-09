namespace TubeSniper.Domain.Proxies
{
	interface IProxyRegister
	{
		ProxyEntry Aquire();
		void Update(ProxyEntry proxy);
		void Remove(ProxyEntry proxy);
	}
}