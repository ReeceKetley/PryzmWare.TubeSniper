using System.Globalization;
using System.Windows.Controls;

namespace TubeSniper.Presentation.Wpf.ValidationRules
{
	class ListViewRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			return ValidationResult.ValidResult;
		}
	}
}
