using System.Collections.Generic;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Domain.Models
{
	public interface IAccountRegister 
	{
		YoutubeAccount Acquire();
		List<AccountRegisterItem> GetAll();
		void Release(YoutubeAccount account);
	}
}