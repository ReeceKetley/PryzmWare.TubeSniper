using System;
using System.Collections.Generic;
using System.Net;
using CSharpFunctionalExtensions;

namespace TubeSniper.Core.Domain.Proxies
{
	public class HttpProxyCredentials : ValueObject
	{
		public HttpProxyCredentials(string username, string password)
		{
			CheckIsValid(username, password, true);
			Username = username;
			Password = password;
		}

		public string Username { get; }

		public string Password { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Username;
			yield return Password;
		}

		public static bool TryCreate(string username, string password, out HttpProxyCredentials result)
		{
			if (!CheckIsValid(username, password, false))
			{
				result = null;
				return false;
			}

			result = new HttpProxyCredentials(username, password);
			return true;
		}

		private static bool CheckIsValid(string username, string password, bool throwExceptions)
		{
			if (username == null)
			{
				if (throwExceptions)
				{
					throw new ArgumentNullException(nameof(username));
				}

				return false;
			}

			if (password == null)
			{
				if (throwExceptions)
				{
					throw new ArgumentNullException(nameof(password));
				}

				return false;
			}

			if (username.Length < 1)
			{
				if (throwExceptions)
				{
					throw new ArgumentException("Value must not be empty.", nameof(username));
				}

				return false;
			}

			if (password.Length < 1)
			{
				if (throwExceptions)
				{
					throw new ArgumentException("Value must not be empty.", nameof(password));
				}

				return false;
			}

			return true;
		}

		public NetworkCredential ToNetworkCredentials()
		{
			var credentials = new NetworkCredential(Username, Password);
			return credentials;
		}
	}
}
