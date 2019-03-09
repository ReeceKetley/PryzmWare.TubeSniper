using System.Collections.Generic;
using TubeSniper.Domain.Youtube;

namespace TubeSniper.Domain.Models
{
	public interface IAccountRegister 
	{
		YoutubeAccount Acquire();
		List<AccountRegisterItem> GetAll();
		void Release(YoutubeAccount account);
	}
}