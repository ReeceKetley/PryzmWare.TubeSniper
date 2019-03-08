using System;
using TubeSniper.Core;
using TubeSniper.Infrastructure.Models;
using TubeSniper.Infrastructure.Repositories;

namespace TubeSniper.Infrastructure
{
	public static class SettingsStorage
	{
		public static ApplicationSettings AppSettings = DatabaseManager.GetSettings();
		public static int MinTypeSpeed { get; set; } = AppSettings.MinTypingSpeed;
		public static int MaxTypeSpeed { get; set; } = AppSettings.MaxTypingSpeed;
	}
}
