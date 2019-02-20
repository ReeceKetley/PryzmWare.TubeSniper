using System;
using System.Windows.Forms;
using TubeSniper.Core.Domain;
using TubeSniper.Core.Domain.Browser;
using TubeSniper.DependencyResolution;
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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DebugView());
		}
	}
}