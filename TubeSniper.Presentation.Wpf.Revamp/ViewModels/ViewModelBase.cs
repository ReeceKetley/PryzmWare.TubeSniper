using System.ComponentModel;

namespace TubeSniper.Presentation.Wpf.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
	}
}