using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using TubeSniper.Domain.Common.Extensions;

namespace TubeSniper.Domain.Auth
{
	public class ProductKey : ValueObject
	{
		public ProductKey(string value)
		{
			CheckIsValid(ref value, true);
			Value = value;
		}

		public string Value { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static bool TryCreate(string value, out ProductKey result)
		{
			if (!CheckIsValid(ref value, false))
			{
				result = null;
				return false;
			}

			result = new ProductKey(value);
			return true;
		}

		private static bool CheckIsValid(ref string value, bool throwExceptions)
		{
			if (value == null)
			{
				if (throwExceptions)
				{
					throw new ArgumentNullException(nameof(value));
				}

				return false;
			}

			if (value.Length < 1)
			{
				if (throwExceptions)
				{
					throw new ArgumentException("Value must not be empty.", nameof(value));
				}

				return false;
			}

			value = value.ToUpper();

			if (!Regex.IsMatch(value, "^(?:[A-Z0-9]{4}-){6}(?:[A-Z0-9]{2})TA$") && !Regex.IsMatch(value, "^[A-Z0-9]{26}TA$"))
			{
				if (throwExceptions)
				{
					throw new ArgumentException("Value must be valid license key.", nameof(value));
				}

				return false;
			}

			var clean = value.Replace("-", "");
			var parts = clean.Split(4);
			value = string.Join("-", parts);

			return true;
		}
	}
}