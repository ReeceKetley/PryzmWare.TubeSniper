using System;
using System.Drawing;
using System.Net;
using System.Threading;
using Pyrzm.AntiCaptchaClient;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Domain.Models.States;
using TubeSniper.Core.Domain.Models.States.V0;
using TubeSniper.Core.Domain.Youtube;

namespace TubeSniper.Core.Services
{
	public class YoutubeLoginBot
	{
		private readonly YoutubeAccount _account;
		private readonly YoutubeBrowser _browser;

		public YoutubeLoginBot(YoutubeBrowser browser, YoutubeAccount account)
		{
			_browser = browser;
			_account = account;
		}

		public LoginResult Run()
		{
			Console.WriteLine("Called Run");
			return Login(_browser, _account);
		}

		public LoginResult Login(YoutubeBrowser browser, YoutubeAccount account)
		{
			var loadSignInResult = LoadSignIn(browser);
			Console.WriteLine(loadSignInResult.Code);
			if (loadSignInResult.Code == LoadSignInResultCode.AlreadyLoggedIn)
			{
				return new LoginResult(LoginResultCode.Success);
			}

			if (loadSignInResult.Code != LoadSignInResultCode.Success)
			{
				return new LoginResult(LoginResultCode.HttpError, browser.Proxy);
			}


			for (;; Thread.Sleep(500))
			{
				//Console.ReadLine();
				var page = GetLoginState();
				var pageUrl = browser.Browser.WebView.Url;
				Console.WriteLine(page);

				switch (page)
				{
					case LoginEmailPage loginEmailPage:
					{
						Console.WriteLine("Login Email Page");
						HandleLoginEmailState(browser, account, loginEmailPage);
						break;
					}
					case LoginPasswordPage loginPasswordPage:
					{
						Console.WriteLine("Login Password Page");
						HandleLoginPasswordState(browser, account, pageUrl, loginPasswordPage);
						if (browser.Browser.WebView.GetEvalBool("(typeof jQuery != 'undefined') && $(\"input[type=\\\"password\\\"]\").attr(\"aria-invalid\") == \"true\""))
						{
							return new LoginResult(LoginResultCode.BadCredentials);
						}

						break;
					}
					case LoginCaptchaPage loginCaptchaPage:
					{
						Console.WriteLine("Login Captcha Page");
						HandleLoginCaptchaState(account, loginCaptchaPage, browser);
						break;
					}
					case LoginSelectRecoveryPage loginSelectRecoveryPage:
					{
						Console.WriteLine("Login Select Recovery Page");
						HandleRecoverySelectState(loginSelectRecoveryPage);
						break;
					}
					case LoginSubmitRecoveryPage loginSubmitRecoveryPage:
					{
						Console.WriteLine("Login Submit Recovery Page");
						HandleSubmitRecoveryState(account, loginSubmitRecoveryPage);
						if (browser.Browser.WebView.GetEvalBool("(typeof jQuery != 'undefined') && $(\"input[type=\\\"text\\\"]\").attr(\"aria-invalid\") == \"true\""))
						{
							return new LoginResult(LoginResultCode.BadRecoveryCredentials);
						}

						break;
					}


					case LoginAccountSuspended loginAccountSuspended:
					{
						Console.WriteLine("Account Susspended");
						return new LoginResult(LoginResultCode.AccountSuspended);
					}
					case LoginAccountNotFound loginAccountNotFound:
					{
						Console.WriteLine("Account Not found");
						return new LoginResult(LoginResultCode.AccountNotFound);
					}

					default:
					{
						if (browser.Browser.WebView.Url.StartsWith("https://www.youtube.com/"))
						{
							return new LoginResult(LoginResultCode.Success);
						}

						continue;
					}
				}
			}
		}

		private static void HandleSubmitRecoveryState(YoutubeAccount account, LoginSubmitRecoveryPage loginSubmitRecoveryPage)
		{
			loginSubmitRecoveryPage.SetRecoveryEmail(account.RecoveryEmail);
			loginSubmitRecoveryPage.Submit();
		}

		private static void HandleRecoverySelectState(LoginSelectRecoveryPage loginSelectRecoveryPage)
		{
			loginSelectRecoveryPage.ClickEmail();
		}

		private void HandleLoginCaptchaState(YoutubeAccount account, LoginCaptchaPage loginCaptchaPage, YoutubeBrowser browser)
		{
			var img = loginCaptchaPage.GetImageUrl();
			Image captchaImage = null;
			try
			{
				using (var wc = new WebClient())
				{
					captchaImage = ImageHelper.ImageFromBytes(wc.DownloadData(img));
				}
			}
			catch
			{
				Login(browser, account);
			}

			var antiCaptcha = AntiCaptchaHelper.SolveCaptcha(captchaImage, "62362c3187702f77b13610b02fa96df4", TimeSpan.FromSeconds(30));
			loginCaptchaPage.SetToken(antiCaptcha.Result);
			loginCaptchaPage.SetPassword(account.Credentials.Password);
			loginCaptchaPage.Submit();
		}

		private static void HandleLoginEmailState(YoutubeBrowser browser, YoutubeAccount account, LoginEmailPage loginEmailPage)
		{
			loginEmailPage.SubmitEmail(account.Credentials.Email);
			browser.Browser.WebView.WaitUntilUrlContains("signin", TimeSpan.FromSeconds(30));
		}

		private static void HandleLoginPasswordState(YoutubeBrowser browser, YoutubeAccount account, string pageUrl, LoginPasswordPage loginPasswordPage)
		{
			var checkString = Guid.NewGuid().ToString();
			loginPasswordPage.SetAriaInvalid(checkString);
			loginPasswordPage.SetPassword(account.Credentials.Password);
			loginPasswordPage.Submit();
			for (;; Thread.Sleep(500))
			{
				if (pageUrl != browser.Browser.WebView.Url)
				{
					break;
				}

				if (loginPasswordPage.GetAriaInvalid() != checkString)
				{
					break;
				}
			}
		}

		private LoginState GetLoginState()
		{
			if (LoginEmailPage.Detect(_browser))
			{
				return new LoginEmailPage(_browser.Browser);
			}

			if (LoginCaptchaPage.Detect(_browser))
			{
				return new LoginCaptchaPage(_browser.Browser);
			}

			if (LoginPasswordPage.Detect(_browser))
			{
				return new LoginPasswordPage(_browser.Browser);
			}

			if (LoginSelectRecoveryPage.Detect(_browser))
			{
				return new LoginSelectRecoveryPage(_browser.Browser);
			}

			if (LoginSubmitRecoveryPage.Detect(_browser))
			{
				return new LoginSubmitRecoveryPage(_browser.Browser);
			}

			if (LoginAccountSuspended.Detect(_browser))
			{
				return new LoginAccountSuspended(_browser.Browser);
			}

			if (LoginAccountNotFound.Detect(_browser))
			{
				return new LoginAccountNotFound(_browser.Browser);
			}

			return null;
		}

		private LoadSignInResult LoadSignIn(YoutubeBrowser browser)
		{
			Console.WriteLine("Loading Youtube");
			browser.Browser.WebView.LoadUrl("https://www.youtube.com/");
			if (!browser.Browser.WebView.Url.Contains("youtube.com"))
			{
				return new LoadSignInResult(LoadSignInResultCode.LoadYoutubeError);
			}

			if (browser.Browser.WebView.ElementExists("#avatar-btn") || browser.Browser.WebView.Url.Contains("general-light"))
			{
				return new LoadSignInResult(LoadSignInResultCode.AlreadyLoggedIn);
			}

			if (!browser.Browser.Mouse.MoveToAndClickElement("a[href*=\\\"ServiceLogin\\\"]"))
			{
				return new LoadSignInResult(LoadSignInResultCode.SignInButtonClickFail);
			}

			if (!browser.Browser.WebView.WaitUntilUrlContains("ServiceLogin", TimeSpan.FromSeconds(30)))
			{
				return new LoadSignInResult(LoadSignInResultCode.LoadLoginTimeout);
			}

			if (!browser.Browser.WebView.WaitUntilUrlContains("https://accounts.google.com/signin/v2/identifier?", TimeSpan.FromSeconds(30)))
			{
				return new LoadSignInResult(LoadSignInResultCode.LoadLoginTimeout);
			}

			return new LoadSignInResult();
		}
	}
}