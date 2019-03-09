using System.Collections.Generic;

namespace TubeSniper.Domain.Services
{
	public interface ISuccessfulCommentManager
	{
		List<SuccessComment> GetComments();
		void Add(SuccessComment comment);
	}
}