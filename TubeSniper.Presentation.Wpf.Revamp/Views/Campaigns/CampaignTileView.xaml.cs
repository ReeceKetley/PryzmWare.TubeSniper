using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro.Controls;
using TubeSniper.Presentation.Wpf.Common;
using TubeSniper.Presentation.Wpf.ViewModels.Campaigns;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace TubeSniper.Presentation.Wpf.Views.Campaigns
{
	/// <summary>
	/// Interaction logic for CampaignControl.xaml
	/// </summary>
	public partial class CampaignTileView : UserControl
	{
		private readonly object _mutext = new object();
		public CampaignTileView()
		{
			InitializeComponent();
			DataContextChanged += OnDataContextChanged;
		}

		private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			{
				return;
			}

			if (!(DataContext is CampaignTileViewModel viewModel))
			{
				return;
			}

			viewModel.StatusLog.CollectionChanged += StatusLogOnCollectionChanged;
			viewModel.WarningFired += ViewModel_WarningFired;
			viewModel.VideoProcessed += ViewModel_VideoProcessed;
		}

		private void ViewModel_VideoProcessed(object sender, EventArgs e)
		{
			ChangeControlColour(System.Drawing.Color.DarkSeaGreen, System.Drawing.Color.CornflowerBlue);
		}

		private void ViewModel_WarningFired(object sender, EventArgs e)
		{
			ChangeControlColour(System.Drawing.Color.Orange, System.Drawing.Color.DarkSeaGreen);
		}

		private void StatusLogOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{

		}

		private void UIElement_OnMouseEnter(object sender, MouseEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(0, 120, 215));
		}

		private void UIElement_OnMouseLeave(object sender, MouseEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
		}

		private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(0, 165, 215));

		}

		private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(0, 120, 215));
		}


		private void ChangeControlColour(System.Drawing.Color activeControl, System.Drawing.Color eventColour)
		{
			uint intervals = 10;

			var colorFader = new ColorFader(eventColour, activeControl, intervals);
			var colorFader2 = new ColorFader(activeControl, eventColour, intervals);

			SetControlBackColor(eventColour);

			/*  LinearFading Process isolated in a seperate Task to avoid blocking UI   */
			Task t = Task.Factory.StartNew(() =>
			{
				System.Threading.Thread.Sleep(500);
				foreach (var color in colorFader.Fade())
				{
					SetControlBackColor(color);
					System.Threading.Thread.Sleep(50);
				}
				foreach (var color in colorFader2.Fade())
				{
					SetControlBackColor(color);
					System.Threading.Thread.Sleep(50);
				}
			});
		}

		private void SetControlBackColor(System.Drawing.Color color)
		{
			lock (_mutext)
			{
				ProgressSpinner.Invoke(delegate
				{
					ProgressSpinner.Foreground = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
					ProgressBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
				});
			}
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{

		}

		private void Edit_MouseEnter(object sender, MouseEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(236, 28, 36));
		}

		private void Edit_MouseLeave(object sender, MouseEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(245, 245, 245));
		}

		private void Edit_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(255, 127, 131));
		}

		private void Edit_MouseUp(object sender, MouseButtonEventArgs e)
		{
			var border = (Border)sender;
			border.Background = new SolidColorBrush(Color.FromRgb(236, 28, 36));
		}
	}
}
