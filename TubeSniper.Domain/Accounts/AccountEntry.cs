using System;
using SoleSlayer.Domain.Customer;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Accounts
{
	public class AccountEntry
	{
		public AccountEntry()
		{
		}

		public AccountEntry(YoutubeCredentials credentials, Email recoveryEmail)
		{
			Credentials = credentials;
			RecoveryEmail = recoveryEmail;
		}

		public Guid Id { get; set; }
		public YoutubeCredentials Credentials { get; set; }
		public Email RecoveryEmail { get; set; }

		public AccountEntry DeepClone()
		{
			return (AccountEntry) MemberwiseClone();
		}

		public bool CheckIsValid()
		{
			if (Credentials == null)
			{
				return false;
			}

			return true;
		}
	}
}