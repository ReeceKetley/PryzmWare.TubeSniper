using Microsoft.Win32;

namespace TubeSniper.Core
{
	public static class RegistryClass
	{
		static RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PryzmWare\TubeSniper");

		public static bool SaveStringSetting(string setting, string value)
		{
			key.
			key.SetValue(setting, value);
			key.Close();
			return true;
		}

		public static string ReadStringSetting(string setting, string def = null)
		{
			if (key != null) return (string) key.GetValue(setting);
			else
			{
				if (def == null)
				{
					return "0";
				}
				else
				{
					return def.Trim();
				}
			}
		}

	}
}
