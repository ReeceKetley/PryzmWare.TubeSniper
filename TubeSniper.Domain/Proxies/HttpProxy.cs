using System;
using System.Collections.Generic;
using System.Net;
using CSharpFunctionalExtensions;

namespace TubeSniper.Domain.Proxies
{
	public class HttpProxy : ValueObject
	{
		public HttpProxy(HttpProxyAddress address, HttpProxyCredentials credentials)
		{
			Address = address ?? throw new ArgumentNullException(nameof(address));
			Credentials = credentials;
		}

		public HttpProxy(HttpProxyAddress address) : this(address, null)
		{
		}

		public HttpProxyAddress Address { get; }
		public HttpProxyCredentials Credentials { get; }

		public HttpProxy DeepClone()
		{
			return (HttpProxy)MemberwiseClone();
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Address;
			yield return Credentials;
		}

		public WebProxy ToWebProxy()
		{
			var address = Address.ToUri();
			if (address == null)
			{
				throw new Exception();
			}

			NetworkCredential credentials = null;
			if (Credentials != null)
			{
				credentials = Credentials.ToNetworkCredentials();
				if (credentials == null)
				{
					throw new Exception();
				}
			}

			return new WebProxy { Address = address, Credentials = credentials, BypassProxyOnLocal = false };
		}
	}
}


