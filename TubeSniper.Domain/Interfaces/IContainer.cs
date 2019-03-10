namespace TubeSniper.Domain.Interfaces
{
	public interface IContainer
	{
		T Resolve<T>() where T : class;
	}
}