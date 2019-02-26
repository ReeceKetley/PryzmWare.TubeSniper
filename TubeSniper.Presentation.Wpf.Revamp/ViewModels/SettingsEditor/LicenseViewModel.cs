using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TubeSniper.Infrastructure.Services;
using TubeSniper.Presentation.Wpf.Commands;

namespace TubeSniper.Presentation.Wpf.ViewModels.SettingsEditor
{
    public class LicenseViewModel : ViewModelBase
    {
	    private readonly AuthService _authService;
	    public string Key { get; set; }

	    public LicenseViewModel(AuthService authService)
	    {
		    _authService = authService;
		    Key = "License Key: " + _authService.GetStoredKey().Value.ToUpper();
	    }

	    private ICommand _deactivateCommand;
	    public ICommand DeactivateCommand => _deactivateCommand ?? (_deactivateCommand = new RelayCommand(DeactivateLicense));

	    private void DeactivateLicense(object obj)
	    {
		    _authService.DeactiveKey();
		    System.Windows.MessageBox.Show("System Deactivated");
			Application.Current.Shutdown(0);
	    }
    }
}
