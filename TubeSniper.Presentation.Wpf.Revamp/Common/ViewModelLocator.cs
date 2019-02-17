using TubeSniper.Core.Common;

namespace TubeSniper.Presentation.Wpf.Common
{
	public static class ViewModelLocator
	{
		public static T Locate<T>() where T : class
		{
			return GlobalContainer.Container.Resolve<T>();
		}
	}
}
