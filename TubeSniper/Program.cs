using System;
using TubeSniper.DependencyResolution;
using TubeSniper.Domain;
using TubeSniper.Domain.Browser;
using TubeSniper.Infrastructure.Services;

namespace TubeSniper
{
	internal static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			//Main2();
			//return;
			Bootstrapper.Main(args);
		}

		private static void Main2()
		{
			var authService = new AuthService();
			Globals.SelectorPayload = authService.GetSelectorPayload("MSF2-459S-WEBA-I2GM-QUZF-CTF7-Z6TA");
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			System.Windows.Forms.Application.Run(new DebugView());
		}
	}
}