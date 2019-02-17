using System.Globalization;
using System.Windows.Controls;

namespace TubeSniper.Presentation.Wpf.ValidationRules
{
	class ProxyValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			var proxy = (string) value;
			var ValidIpAddressPortRegex = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]):[\d]+$";
			if (System.Text.RegularExpressions.Regex.IsMatch(proxy, ValidIpAddressPortRegex))
			{
				return ValidationResult.ValidResult;
			}

			return new ValidationResult(false, "Value isn't a valid proxy string. (IP:Port format expected)");
		}
	}
}
