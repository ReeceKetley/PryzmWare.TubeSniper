using System.Windows;
using TubeSniper.Presentation.Wpf.ViewModels.ProxyEditor;

namespace TubeSniper.Presentation.Wpf.Views.ProxyEditor
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
