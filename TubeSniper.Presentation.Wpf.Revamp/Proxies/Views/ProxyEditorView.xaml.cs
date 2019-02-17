using System.Windows;
using TubeSniper.Presentation.Wpf.Proxies.ViewModels;

namespace TubeSniper.Presentation.Wpf.Proxies.Views
{
	/// <summary>
	/// Interaction logic for CreateProxyView.xaml
	/// </summary>
	public partial class ProxyEditorView : Window
	{
		public ProxyEditorView(ProxyEditorViewModel viewModel)
		{
			InitializeComponent();
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}
			DataContext = viewModel;

			if (viewModel.CloseAction == null)
			{
				viewModel.CloseAction = Close;
			}
		}
	}
}
