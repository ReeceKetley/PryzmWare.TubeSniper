using System;
using TubeSniper.DependencyResolution;

namespace TubeSniper
{
	internal static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Bootstrapper.Main(args);
		}
	}
}