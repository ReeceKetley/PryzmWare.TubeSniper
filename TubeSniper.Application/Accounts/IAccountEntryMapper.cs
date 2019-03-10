using TubeSniper.Domain.Accounts;

namespace TubeSniper.Application.Accounts
{
	public interface IAccountEntryMapper
	{
		AccountEntry Map(AccountEntryDto dto);
		AccountEntryDto Map(AccountEntry model);
	}
}