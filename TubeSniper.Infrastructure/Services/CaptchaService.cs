using System;
using System.Drawing;
using System.Net;
using Pyrzm.AntiCaptchaClient;
using TubeSniper.Domain.Services;

namespace TubeSniper.Infrastructure.Services
{
	public class CaptchaService : ICaptchaService
	{
		public string SolveCaptcha(Uri imageUrl)
		{
			Image captchaImage;
			try
			{
				using (var wc = new WebClient())
				{
					captchaImage = ImageHelper.ImageFromBytes(wc.DownloadData(imageUrl));
				}
			}
			catch
			{
				return null;
			}

			return AntiCaptchaHelper.SolveCaptcha(captchaImage, "62362c3187702f77b13610b02fa96df4", TimeSpan.FromSeconds(30)).Result;
		}
	}
}
