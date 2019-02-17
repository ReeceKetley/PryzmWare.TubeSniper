using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TubeSniper.Presentation.Wpf.ViewModels.CampaignEditor;

namespace TubeSniper.Presentation.Wpf.Views.CampaignEditor
{
    /// <summary>
    /// Interaction logic for CreateCampaign.xaml
    /// </summary>
    public partial class CampaignEditorView
    {
	    private bool _selcted;
	    private bool _selctedA;
	    public CampaignEditorView(CampaignEditorViewModel viewModel)
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


	    private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
	    {
		    Regex regex = new Regex("[^0-9]+");
		    e.Handled = regex.IsMatch(e.Text);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
