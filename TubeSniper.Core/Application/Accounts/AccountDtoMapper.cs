using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Application.Accounts
{
	public static class AccountDtoMapper
	{
		public static AccountDto ToDto(this YoutubeAccount account)
		{
			var accountDto = new AccountDto();
			accountDto.Id = account.Id;
			accountDto.Username = account.Credentials.Email;
			accountDto.Password = account.Credentials.Password;
			accountDto.RecoveryEmail = account.RecoveryEmail;
			return accountDto;
		}

		public static YoutubeAccount FromDto(this AccountDto accountDto)
		{
			var account = new YoutubeAccount(new YoutubeCredentials(accountDto.Username, accountDto.Password), accountDto.RecoveryEmail);
			account.Id = accountDto.Id;
			return account;
		}
	}
}
