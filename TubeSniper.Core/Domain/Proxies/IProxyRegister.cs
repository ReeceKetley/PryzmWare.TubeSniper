﻿namespace TubeSniper.Core.Domain.Proxies
{
	interface IProxyRegister
	{
		ProxyEntry Aquire();
		void Update(ProxyEntry proxy);
	}
}