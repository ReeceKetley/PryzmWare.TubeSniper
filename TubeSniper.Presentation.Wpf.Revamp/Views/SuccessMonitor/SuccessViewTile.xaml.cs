using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using TubeSniper.Presentation.Wpf.ViewModels.SuccessMonitor;

namespace TubeSniper.Presentation.Wpf.Views.SuccessMonitor
{
    /// <summary>
    /// Interaction logic for SuccessViewTile.xaml
    /// </summary>
    public partial class SuccessViewTile : UserControl
    {
        public SuccessViewTile()
        {
            InitializeComponent();
			DataContextChanged += SuccessViewTile_DataContextChanged;
		}

		private void SuccessViewTile_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}

			if (!(DataContext is SuccessViewTileViewModel viewModel))
			{
				return;
			}
		}

	    private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	    {
		    if (UrlBox.Text != null)
		    {
			    Process.Start(UrlBox.Text);
		    }
	    }
    }
}
