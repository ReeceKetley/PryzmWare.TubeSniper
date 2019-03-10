using EO.Internal;
using SoleSlayer.Domain.Customer;
using TubeSniper.Domain.Accounts;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Accounts
{
	public class AccountEntryMapper : IAccountEntryMapper
	{
		public AccountEntry Map(AccountEntryDto dto)
		{
			var model = new AccountEntry();
			model.Id = dto.Id;
			YoutubeUsername username = null;
			if (!string.IsNullOrEmpty(dto.CredentialsUsername))
			{
				if (!YoutubeUsername.TryCreate(dto.CredentialsUsername, out username))
				{
					return null;
				}
			}

			YoutubePassword password = null;
			if (!string.IsNullOrEmpty(dto.CredentialsPassword))
			{
				if (!YoutubePassword.TryCreate(dto.CredentialsPassword, out password))
				{
					return null;
				}
			}

			if (username != null || password != null)
			{
				if (!YoutubeCredentials.TryCreate(username, password, out var credentials))
				{
					return null;
				}

				model.Credentials = credentials;
			}

			if (!string.IsNullOrEmpty(dto.RecoveryEmail))
			{
				if (!Email.TryCreate(dto.RecoveryEmail, out var email))
				{
					return null;
				}

				model.RecoveryEmail = email;
			}

			if (!model.CheckIsValid())
			{
				return null;
			}

			return model;
		}

		public AccountEntryDto Map(AccountEntry model)
		{
			var dto = new AccountEntryDto();
			dto.Id = model.Id;
			dto.CredentialsUsername = model.Credentials?.Username.Value;
			dto.CredentialsPassword = model.Credentials?.Password.Value;
			dto.RecoveryEmail = model.RecoveryEmail?.Value;
			return dto;
		}
	}
}