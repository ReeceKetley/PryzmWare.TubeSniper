using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace TubeSniper.Presentation.Wpf.Themes.Controls
{
	/// <summary>
	/// Interaction logic for RatingControl.xaml
	/// </summary>
	public partial class RatingControl : UserControl
	{
		public RatingControl()
		{
			InitializeComponent();
			DataContext = this;
		}
		public int RatingValue
		{
			get { return (int)GetValue(RatingValueProperty); }
			set { SetValue(RatingValueProperty, value); }
		}
		public static readonly DependencyProperty RatingValueProperty =
			DependencyProperty.Register("RatingValue", typeof(int), typeof(RatingControl), new UIPropertyMetadata(0));
	}

	public class RatingConverter : IValueConverter
	{
		public Brush OnBrush { get; set; }
		public Brush OffBrush { get; set; }

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			int rating = 0;
			int number = 0;
			if (int.TryParse(value.ToString(), out rating) && int.TryParse(parameter.ToString(), out number))
			{
				if (rating >= number)
				{
					return OnBrush;
				}
				return OffBrush;
			}
			return Brushes.Transparent;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
