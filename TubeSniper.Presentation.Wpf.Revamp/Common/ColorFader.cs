using System;
using System.Collections.Generic;
using System.Drawing;

namespace TubeSniper.Presentation.Wpf.Common
{
	public class ColorFader
	{
		private readonly Color _From;
		private readonly Color _To;

		private readonly double _StepR;
		private readonly double _StepG;
		private readonly double _StepB;

		private readonly uint _Steps;

		public ColorFader(Color from, Color to, uint steps)
		{
			if (steps == 0)
				throw new ArgumentException("steps must be a positive number");

			_From = from;
			_To = to;
			_Steps = steps;

			_StepR = (double)(_To.R - _From.R) / _Steps;
			_StepG = (double)(_To.G - _From.G) / _Steps;
			_StepB = (double)(_To.B - _From.B) / _Steps;
		}

		public IEnumerable<Color> Fade()
		{
			for (uint i = 0; i < _Steps; ++i)
			{
				yield return Color.FromArgb((int)(_From.R + i * _StepR), (int)(_From.G + i * _StepG), (int)(_From.B + i * _StepB));
			}
			yield return _To; // make sure we always return the exact target color last
		}
	}
}