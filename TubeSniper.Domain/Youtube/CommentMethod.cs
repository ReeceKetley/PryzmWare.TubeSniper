using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TubeSniper.Domain.Youtube
{
	public class CommentMethod : ValueObject
	{
		protected CommentMethod(string value)
		{
			Value = value;
		}

		public string Value { get; }

		public static CommentMethod Reply { get; } = new CommentMethod("reply");
		public static CommentMethod Comment { get; } = new CommentMethod("comment");

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static bool TryParse(string value, out CommentMethod result)
		{
			if (value == null)
			{
				result = null;
				return false;
			}

			if (value.Equals(Reply.Value, StringComparison.OrdinalIgnoreCase))
			{
				result = Reply;
				return true;
			}

			if (value.Equals(Comment.Value, StringComparison.OrdinalIgnoreCase))
			{
				result = Comment;
				return true;
			}

			result = null;
			return false;
		}
	}
}