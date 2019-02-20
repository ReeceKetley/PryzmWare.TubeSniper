using System;
using System.Drawing;
using System.Net;
using System.Threading;
using Pyrzm.AntiCaptchaClient;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Core.Domain.Models.States;
using TubeSniper.Core.Domain.Models.States.V1;

namespace TubeSniper.Core.Domain.Youtube
{
	class YoutubeLoginBotV1
	{
		private readonly YoutubeBrowser _browser;
		private readonly YoutubeAccount _account;

		public YoutubeLoginBotV1(YoutubeBrowser browser, YoutubeAccount account)
		{
			_browser = browser;
			_account = account;
		}

		public LoginResult Run()
		{
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



			for (; ; Thread.Sleep(500))
			{
				//Console.ReadLine();
				var page = GetLoginState();
				var pageUrl = browser.Browser.WebView.Url;
				Console.WriteLine(pageUrl);
				switch (page)
				{
					case LoginEmailPageV1 loginEmailPage:
						{
							Console.WriteLine("Login Email Page");
							HandleLoginEmailState(browser, account, loginEmailPage);
							break;
						}
					case LoginPasswordPageV1 loginPasswordPage:
						{
							Console.WriteLine("Login Password Page");
							HandleLoginPasswordState(browser, account, pageUrl, loginPasswordPage);
							break;
						}
					case LoginUpgradeBrowserPageV1 loginUpgradeBrowserPage:
						{
							Console.WriteLine("Login Upgrade Browser");
							HandleLoginUpgradeBrowser(browser, loginUpgradeBrowserPage);
							break;
						}
					case LoginCaptchaPageV1 loginCaptchaPage:
						{
							Console.WriteLine("Login Captcha Page");
							HandleLoginCaptchaState(account, loginCaptchaPage, browser);
							break;
						}
					case LoginSelectRecoveryPageV1 loginSelectRecoveryPage:
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

		private void HandleLoginUpgradeBrowser(YoutubeBrowser browser, LoginUpgradeBrowserPageV1 loginUpgradeBrowserPage)
		{
			loginUpgradeBrowserPage.SetUserAgent(browser);
		}

		private static void HandleSubmitRecoveryState(YoutubeAccount account, LoginSubmitRecoveryPage loginSubmitRecoveryPage)
		{
			loginSubmitRecoveryPage.SetRecoveryEmail(account.RecoveryEmail);
			loginSubmitRecoveryPage.Submit();
		}

		private static void HandleRecoverySelectState(LoginSelectRecoveryPageV1 loginSelectRecoveryPage)
		{
			loginSelectRecoveryPage.SelectRecoveryEmail();
		}

		private void HandleLoginCaptchaState(YoutubeAccount account, LoginCaptchaPageV1 loginCaptchaPage, YoutubeBrowser browser)
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

		private static void HandleLoginEmailState(YoutubeBrowser browser, YoutubeAccount account, LoginEmailPageV1 loginEmailPage)
		{
			loginEmailPage.SubmitEmail(account.Credentials.Email);
			browser.Browser.WebView.WaitUntilUrlContains("ServiceLogin?sacu=1#identifier", TimeSpan.FromSeconds(30));
		}

		private static void HandleLoginPasswordState(YoutubeBrowser browser, YoutubeAccount account, string pageUrl, LoginPasswordPageV1 loginPasswordPage)
		{
			loginPasswordPage.SetPassword(account.Credentials.Password);
			loginPasswordPage.Submit();
			for (; ; Thread.Sleep(500))
			{
				if (pageUrl != browser.Browser.WebView.Url)
				{
					break;
				}
			}
		}

		private LoginState GetLoginState()
		{
			if (LoginEmailPageV1.Detect(_browser))
			{
				return new LoginEmailPageV1(_browser.Browser);
			}

			if (LoginCaptchaPage.Detect(_browser))
			{
				return new LoginCaptchaPage(_browser.Browser);
			}

			if (LoginPasswordPageV1.Detect(_browser))
			{
				return new LoginPasswordPageV1(_browser.Browser);
			}

			if (LoginSelectRecoveryPageV1.Detect(_browser))
			{
				return new LoginSelectRecoveryPageV1(_browser.Browser);
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

			if (LoginUpgradeBrowserPageV1.Detect(_browser))
			{
				return new LoginUpgradeBrowserPageV1(_browser.Browser);
			}

			return null;
		}

		private LoadSignInResult LoadSignIn(YoutubeBrowser browser)
		{
			browser.Browser.WebView.CustomUserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";
			browser.Browser.WebView.LoadUrlAndWait("https://accounts.google.com/ServiceLogin?sacu=1#identifier");

			if (!browser.Browser.WebView.WaitUntilUrlContains("ServiceLogin", TimeSpan.FromSeconds(30)))
			{
				return new LoadSignInResult(LoadSignInResultCode.LoadLoginTimeout);
			}

			if (!browser.Browser.WebView.WaitUntilUrlContains("https://accounts.google.com/ServiceLogin", TimeSpan.FromSeconds(30)))
			{
				return new LoadSignInResult(LoadSignInResultCode.LoadLoginTimeout);
			}

			return new LoadSignInResult();
		}
	}
}

