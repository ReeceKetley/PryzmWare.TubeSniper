using System;

namespace TubeSniper.Domain.Services
{
	public interface ICaptchaService
	{
		string SolveCaptcha(Uri imageUrl);
	}
}