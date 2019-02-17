using Microsoft.Win32;

namespace TubeSniper.Core
{
	public static class RegistryClass
	{
		static RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PryzmWare\TubeSniper");

		public static bool SaveStringSetting(string settting, string value)
		{
			key.SetValue(settting, value);
			key.Close();
			return true;
		}

		public static string ReadStringSetting(string setting)
		{
			if (key != null) return (string) key.GetValue(setting);
			else
			{
				return "0";
			}
		}

	}
}
