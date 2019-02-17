using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TubeSniper.Core.Domain.Youtube
{
    public class CommentTemplate
    {
        private readonly string _message;
        private Random _random;

        public CommentTemplate(string message)
        {
            _random = new Random();
            _message = message;
        }

        public static CommentTemplate FromString(string message)
        {
            return new CommentTemplate(message);
        }

        public string Generate(Dictionary<string, string> variables)
        {
            var message = _message;
            var matches = Regex.Matches(message, @"\{(.*?)\}");
            var replacements = new List<string>();
            var tokens = new List<Match>();
            foreach (Match match in matches)
            {
                tokens.Add(match);
                var conditional = match.Groups[1].Value;
                var parts = conditional.Split('|');
                if (parts.Length == 1)
                {
                    if (_random.Next(2) == 1)
                    {
                        replacements.Add(parts[0]);
                    }
                    else
                    {
                        replacements.Add("");
                    }
                }
                else
                {
                    replacements.Add(parts[_random.Next(parts.Length)]);
                }
            }

            tokens.Reverse();
            for (var index = 0; index < tokens.Count; index++)
            {
                var token = tokens[index];
                var replacement = replacements[replacements.Count - index - 1];
                message = message.Remove(token.Groups[0].Index, token.Groups[0].Length);
                message = message.Insert(token.Groups[0].Index, replacement);
            }

            foreach (var variable in variables)
            {
                message = message.Replace("$" + variable.Key + "$", variable.Value);
            }

            return message;
        }
    }

    public class CommentRegister
    {
        private readonly string _message;
        private readonly Dictionary<string, string> _variables;
        private readonly CommentTemplate commentTemplate;
        public CommentRegister(string message, Dictionary<string, string> variables)
        {
            _message = message;
            _variables = variables;
            commentTemplate = new CommentTemplate(message);
        }

        public string Generate()
        {
            return commentTemplate.Generate(_variables);
        }

    }
}
