using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace TubeSniper.Domain.Proxies
{
	public class HttpProxyAddress : ValueObject
	{
		public HttpProxyAddress(string value)
		{
			CheckIsValid(value, out var host, out var port, true);
			Host = host;
			Port = port;
		}

		protected HttpProxyAddress(string host, int port)
		{
			CheckIsValid(host, port, true);
			Host = host;
			Port = port;
		}

		public string Host { get; }

		public int Port { get; }

		public Uri ToUri()
		{
			if (!Uri.TryCreate("http://" + Host + ":" + Port, UriKind.Absolute, out var address))
			{
				throw new Exception();
			}

			return address;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Host;
			yield return Port;
		}

		public override string ToString()
		{
			return Host + ":" + Port;
		}


		public static bool TryCreate(string value, out HttpProxyAddress result)
		{
			if (!CheckIsValid(value, out var host, out var port, false))
			{
				result = null;
				return false;
			}

			result = new HttpProxyAddress(host, port);
			return true;
		}

		private static bool CheckIsValid(string value, out string host, out int port, bool throwExceptions)
		{
			host = null;
			port = 0;

			if (value == null)
			{
				if (throwExceptions)
				{
					throw new ArgumentNullException(nameof(value));
				}

				return false;
			}

			if (value.Length < 1)
			{
				if (throwExceptions)
				{
					throw new ArgumentException("Value must not be empty.", nameof(value));
				}

				return false;
			}

			if (!ExtractHostPort(value, out host, out port))
			{
				if (throwExceptions)
				{
					throw new ArgumentException("Value was not in a correct format.", nameof(value));
				}

				return false;
			}

			return true;
		}

		private static bool ExtractHostPort(string address, out string host, out int port)
		{
			host = null;
			port = 0;

			var match = Regex.Match(address, @"^(.+):(.+)$");
			if (!match.Success)
			{
				return false;
			}

			var prefix = match.Groups[1].Value;
			var suffix = match.Groups[2].Value;

			if (!Uri.TryCreate("http://" + prefix + ":" + suffix, UriKind.Absolute, out var uri))
			{
				return false;
			}

			if (!uri.Host.Equals(prefix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}

			host = uri.Host;
			port = uri.Port;
			return true;
		}

		private static bool CheckIsValid(string host, int port, bool throwExceptions)
		{
			if (host == null)
			{
				if (throwExceptions)
				{
					throw new ArgumentNullException(nameof(host));
				}

				return false;
			}

			if (host.Length < 1)
			{
				if (throwExceptions)
				{
					throw new ArgumentException("Value must not be empty.", nameof(host));
				}

				return false;
			}

			return CheckIsValid(host + ":" + port, out _, out _, throwExceptions);
		}
	}
}