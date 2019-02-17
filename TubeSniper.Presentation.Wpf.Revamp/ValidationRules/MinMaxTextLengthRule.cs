using System.Globalization;
using System.Windows.Controls;

namespace TubeSniper.Presentation.Wpf.ValidationRules
{
	class MinMaxTextLengthRule : ValidationRule
	{
		public int Min { get; set; }
		public int Max { get; set; }

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var text = (string)value;
			if (string.IsNullOrEmpty(text))
			{
				return new ValidationResult(false, "Value cannot be empty.");
			}

			if (text.Length < Min)
			{
				return new ValidationResult(false, "Value length cannot be less than " + Min + " characters.");
			}

			if (text.Length > Max)
			{
				return new ValidationResult(false, "Value length cannot exceed " + Max + " characters.");
			}

			return new ValidationResult(true, null);
		}
	}
}