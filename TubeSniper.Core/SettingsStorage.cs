﻿using System;

namespace TubeSniper.Core
{
	public static class SettingsStorage
	{
		public static int MinTypeSpeed { get; set; } = Convert.ToInt32(RegistryClass.ReadStringSetting("TypeSpeedMin", "5"));
		public static int MaxTypeSpeed { get; set; } = Convert.ToInt32(RegistryClass.ReadStringSetting("TypeSpeedMax", "250"));
	}
}
