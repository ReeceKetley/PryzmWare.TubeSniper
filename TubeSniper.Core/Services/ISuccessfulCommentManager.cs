using System.Collections.Generic;

namespace TubeSniper.Core.Services
{
	public interface ISuccessfulCommentManager
	{
		List<SuccessComment> GetComments();
		void Add(SuccessComment comment);
	}
}