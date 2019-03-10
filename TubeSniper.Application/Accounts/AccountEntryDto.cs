using System;

namespace TubeSniper.Application.Accounts
{
	public class AccountEntryDto
	{
		public Guid Id { get; set; }
		public string CredentialsUsername { get; set; }
		public string CredentialsPassword { get; set; }
		public string RecoveryEmail { get; set; }
	}
}