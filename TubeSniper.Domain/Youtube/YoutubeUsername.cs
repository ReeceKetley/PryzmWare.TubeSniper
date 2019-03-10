using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace TubeSniper.Domain.Youtube
{
	public class YoutubeUsername : ValueObject
	{
		public YoutubeUsername(string value)
		{
			CheckIsValid(value, true);
			Value = value;
		}

		public string Value { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static bool TryCreate(string value, out YoutubeUsername result)
		{
			if (!CheckIsValid(value, false))
			{
				result = null;
				return false;
			}

			result = new YoutubeUsername(value);
			return true;
		}

		private static bool CheckIsValid(string value, bool throwExceptions)
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
					throw new ArgumentException("Value must not be empty.");
				}

				return false;
			}

			// TODO: email regex
			if (!Regex.IsMatch(value, @"^(.+)@(.+)$"))
			{
				if (throwExceptions)
				{
					throw new FormatException("Value was not in a correct format.");
				}

				return false;
			}

			return true;
		}
	}
}