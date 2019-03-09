namespace TubeSniper.Domain.Proxies
{
	public class ProxyTestResult
	{
		public bool Active { get; }
		public int ResponseTime { get; }

		public ProxyTestResult(bool active, int responseTime = 0)
		{
			Active = active;
			ResponseTime = responseTime;
		}
	}
}