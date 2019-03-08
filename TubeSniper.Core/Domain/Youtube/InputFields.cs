using System.Collections.Generic;

namespace TubeSniper.Core.Domain.Youtube
{
	public class InputFields
	{
		public Dictionary<string, string> Attributes;
		public string Source;

		public InputFields(Dictionary<string, string> attributes, string source)
		{
			Attributes = attributes;
			Source = source;
		}
	}
}