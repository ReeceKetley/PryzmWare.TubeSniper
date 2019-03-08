using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TubeSniper.Core;
using TubeSniper.Infrastructure.Services;
using TubeSniper.Presentation.Wpf.Commands;

namespace TubeSniper.Presentation.Wpf.ViewModels.SettingsEditor
{
    public class SettingsViewModel : ViewModelBase
    {
	    public string MinTypeSpeed { get; set; } = "0";
	    public string MaxTypeSpeed { get; set; } = "200";

		private ICommand _clearCache;
		private ICommand _saveCommand;
	    public ICommand ClearCacheCommand => _clearCache ?? (_clearCache = new RelayCommand(ClearCache));
	    public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(SaveSettings));

	    private void ClearCache(object obj)
	    {
		    try
		    {
			    Directory.Delete(Path.Combine(Environment.CurrentDirectory, "cache"));
		    }
		    catch
		    {
			    System.Windows.MessageBox.Show("Failed to delete cache folder.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
		    }

		    System.Windows.MessageBox.Show("Cache files have been cleared.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
	    }

	    private void SaveSettings(object obj)
	    {
		    RegistryClass.SaveStringSetting("TypeSpeedMin", MinTypeSpeed);
		    RegistryClass.SaveStringSetting("TypeSpeedMax", MaxTypeSpeed);
		    System.Windows.MessageBox.Show("Settings have been updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

		}
	}
}
