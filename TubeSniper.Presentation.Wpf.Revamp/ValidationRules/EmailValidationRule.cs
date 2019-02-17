using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace TubeSniper.Presentation.Wpf.ValidationRules
{
	class EmailValidationRule : ValidationRule
	{
		public EmailValidationRule()
		{
		}

		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var email = (string)value;
			if (email == null)
			{
				return new ValidationResult(false, "Email cannot be blank.");
			}

			if (email.Length > 100)
			{
				return new ValidationResult(false, "Email cannot exceed 100 characters.");
			}

			if (!EmailValidation().IsMatch(email))
			{
				return new ValidationResult(false, "Email is invalid.");
			}

			return new ValidationResult(true, null);
		}

		public static Regex EmailValidation()
		{
			const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
			const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;

			TimeSpan matchTimeout = TimeSpan.FromSeconds(2);

			try
			{
				if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
				{
					return new Regex(pattern, options, matchTimeout);
				}
			}
			catch
			{
			}

			// Legacy fallback (without explicit match timeout)
			return new Regex(pattern, options);
		}
	}
}
