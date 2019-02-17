namespace TubeSniper.Core.Interfaces
{
	public interface IContainer
	{
		T Resolve<T>() where T : class;
	}
}
