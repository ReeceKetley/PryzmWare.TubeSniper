using System;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Application.Accounts
{
	public interface IAccountService
	{
		void Delete(Guid id);
		void Insert(YoutubeAccount youtubeAccount);
		void Update(YoutubeAccount youtubeAccount);
	}
}