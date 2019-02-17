using SimpleInjector;
using TubeSniper.Core.Interfaces;

namespace TubeSniper.DependencyResolution
{
	internal class MyContainer : IContainer
	{
		private readonly Container _container;

		public MyContainer(Container container)
		{
			_container = container;
		}

		public T Resolve<T>() where T : class
		{
			return _container.GetInstance<T>();
		}
	}
}
