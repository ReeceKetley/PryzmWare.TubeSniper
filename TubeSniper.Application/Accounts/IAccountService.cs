using System;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Application.Accounts
{
	public interface IAccountService
	{
		void Delete(Guid id);
		void Insert(YoutubeAccount youtubeAccount);
		void Update(YoutubeAccount youtubeAccount);
	}
}