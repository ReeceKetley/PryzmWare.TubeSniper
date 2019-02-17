using System;
using System.Collections.Generic;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Interfaces.Persistence
{
	public interface IAccountsRepository
	{
		void Delete(YoutubeAccount youtubeAccount);
		IEnumerable<YoutubeAccount> GetAll();
		YoutubeAccount GetById(Guid id);
		YoutubeAccount GetByIdOrDefault(Guid id);
		void Insert(YoutubeAccount youtubeAccount);
		void Update(YoutubeAccount youtubeAccount);
	}
}