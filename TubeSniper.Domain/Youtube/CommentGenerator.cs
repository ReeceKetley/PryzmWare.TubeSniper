using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TubeSniper.Domain.Campaigns;

namespace TubeSniper.Domain.Youtube
{
	public class CommentGenerator
	{
		private readonly Random _random;
		private readonly CommentTemplate _template;

		public CommentGenerator(CommentTemplate template)
		{
			_random = new Random();
			_template = template;
		}

		public Comment Generate()
		{
			var template = _template;
			var matches = Regex.Matches(template.Value, @"\{(.*?)\}");
			var replacements = new List<string>();
			var tokens = new List<Match>();
			foreach (Match match in matches)
			{
				tokens.Add(match);
				var conditional = match.Groups[1].Value;
				var parts = conditional.Split('|');
				if (parts.Length == 1)
				{
					replacements.Add(_random.Next(2) == 1 ? parts[0] : "");
				}
				else
				{
					replacements.Add(parts[_random.Next(parts.Length)]);
				}
			}

			tokens.Reverse();
			var message = template.Value;
			for (var index = 0; index < tokens.Count; index++)
			{
				var token = tokens[index];
				var replacement = replacements[replacements.Count - index - 1];
				message = message.Remove(token.Groups[0].Index, token.Groups[0].Length);
				message = message.Insert(token.Groups[0].Index, replacement);
			}

			return new Comment(message);
		}
	}
}