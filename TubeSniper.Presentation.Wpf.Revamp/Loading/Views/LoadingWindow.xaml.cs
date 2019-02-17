using System.Windows;
using TubeSniper.Core;

namespace TubeSniper.Presentation.Wpf.Loading.Views
{
	/// <summary>
	/// Interaction logic for LoadingWindow.xaml
	/// </summary>
	public partial class LoadingWindow : Window
	{
		public LoadingWindow()
		{
			InitializeComponent();
			SharedData.Loaded += SharedData_Loaded;
		}

		private void SharedData_Loaded(object sender, bool e)
		{
			this.Close();
		}
	}
}
