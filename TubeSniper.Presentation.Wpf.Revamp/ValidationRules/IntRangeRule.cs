using System;
using System.Globalization;
using System.Windows.Controls;

namespace TubeSniper.Presentation.Wpf.ValidationRules
{
	public class IntRangeRule : ValidationRule
	{
		public IntRangeRule()
		{
		}

		public int Min { get; set; }
		public int Max { get; set; }

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{

			var input = 0;
			try
			{
				if (((string)value).Length > 0)
				{
					input = Int32.Parse((String)value);
				}
			}
			catch (Exception e)
			{
				return new ValidationResult(false, "Illegal characters or " + e.Message);
			}

			if (input > Max)
			{
				return new ValidationResult(false, "Value cannot exceed " + Max + ".");
			}

			if (input < Min)
			{
				return new ValidationResult(false, "Value cannot be lower than " + Min + ".");
			}

			return new ValidationResult(true, null);
		}
	}
}
