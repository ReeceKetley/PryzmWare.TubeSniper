using System.Windows.Controls;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.ViewModels.SettingsEditor;

namespace TubeSniper.Presentation.Wpf.Views.SettingsEditor
{
	/// <summary>
	/// Interaction logic for SettingsPageView.xaml
	/// </summary>
	public partial class SettingsView : UserControl
	{
		public SettingsView()
		{
			InitializeComponent();
			DataContext = ViewModelLocator.Locate<SettingsViewModel>();
		}

		private void RangeSlider_RangeSelectionChanged(object sender, MahApps.Metro.Controls.RangeSelectionChangedEventArgs e)
		{
			
		}
	}
}
