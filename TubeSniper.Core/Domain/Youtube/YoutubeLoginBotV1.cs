using System;
using System.Drawing;
using System.Net;
using System.Threading;
using Pyrzm.AntiCaptchaClient;
using TubeSniper.Core.Common.Extensions;
using TubeSniper.Core.Common.Helpers;
using TubeSniper.Core.Domain.Models.States;
using TubeSniper.Core.Domain.Models.States.V0;
using TubeSniper.Core.Domain.Models.States.V1;

namespace TubeSniper.Core.Domain.Youtube
{
	class YoutubeLoginBotV1
	{
		private readonly YoutubeAccount _account;
		private readonly YoutubeBrowser _browser;

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
			if (loadSignInResult.Code == LoadSignInResultCode.AlreadyLoggedIn)
			{
				return new LoginResult(LoginResultCode.Success);
			}

			if (loadSignInResult.Code != LoadSignInResultCode.Success)
			{
				return new LoginResult(LoginResultCode.HttpError, browser.Proxy);
			}

			for (;; Thread.Sleep(750))
			{
				while (browser.Browser.WebView.IsLoading && !browser.Browser.WebView.CanEvalScript)
				{
					Thread.Sleep(50);
				}

				var page = GetLoginState();
				var pageUrl = browser.Browser.WebView.Url;
				browser.Browser.ImageCapture(GeneralHelpers.MakeValidFileName(pageUrl.Substring(42)));
				switch (page)
				{
					case LoginYoutubeHomeStateV1 loginYoutubeHomeStateV1:
					{
						return new LoginResult(LoginResultCode.Success);
					}
					case LoginProxyErrorV1 loginProxyError:
					{
						return new LoginResult(LoginResultCode.ProxyError);
					}
					case LoginErrorPageV1 loginErrorPage:
					{
						var type = LoginErrorPageV1.GetErrorType(browser.Browser);
						switch (type)
						{
							case LoginFormErrorEnums.AccountNotFound:
								return new LoginResult(LoginResultCode.AccountNotFound);
							case LoginFormErrorEnums.AccountSusspended:
								return new LoginResult(LoginResultCode.AccountSuspended);
							case LoginFormErrorEnums.InvalidCaptcha:
								return new LoginResult(LoginResultCode.CaptchaSolveFail);
							case LoginFormErrorEnums.PasswordInvalid:
								return new LoginResult(LoginResultCode.BadCredentials);
							case LoginFormErrorEnums.SubmitRecoveryFail:
								return new LoginResult(LoginResultCode.BadRecoveryCredentials);
							case LoginFormErrorEnums.UnkownError:
								return new LoginResult(LoginResultCode.Failure);
						}

						break;
					}
					case LoginEmailPageV1 loginEmailPage:
					{
						HandleLoginEmailState(account, loginEmailPage);
						break;
					}
					case LoginPasswordPageV1 loginPasswordPage:
					{
						HandleLoginPasswordState(browser, account, pageUrl, loginPasswordPage);
						break;
					}
					case LoginUpgradeBrowserPageV1 loginUpgradeBrowserPage:
					{
						HandleLoginUpgradeBrowser(browser, loginUpgradeBrowserPage);
						break;
					}
					case LoginCaptchaPageV1 loginCaptchaPage:
					{
						HandleLoginCaptchaState(account, loginCaptchaPage, browser);
						break;
					}
					case LoginSelectRecoveryPageV1 loginSelectRecoveryPage:
					{
						HandleRecoverySelectState(loginSelectRecoveryPage);
						break;
					}
					case LoginSubmitRecoveryPageV1 loginSubmitRecoveryPage:
					{
						HandleSubmitRecoveryState(account, loginSubmitRecoveryPage);
						break;
					}


					case LoginAccountSuspended loginAccountSuspended:
					{
						return new LoginResult(LoginResultCode.AccountSuspended);
					}
					case LoginAccountNotFound loginAccountNotFound:
					{
						return new LoginResult(LoginResultCode.AccountNotFound);
					}

					default:
					{
						if (browser.Browser.WebView.Url.Contains("youtube.com/"))
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

		private static void HandleSubmitRecoveryState(YoutubeAccount account, LoginSubmitRecoveryPageV1 loginSubmitRecoveryPage)
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
			loginCaptchaPage.SetPassword(account.Credentials.Password);
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
			loginCaptchaPage.Submit();
		}

		private static void HandleLoginEmailState(YoutubeAccount account, LoginEmailPageV1 loginEmailPage)
		{
			loginEmailPage.SubmitEmail(account.Credentials.Email);
		}

		private static void HandleLoginPasswordState(YoutubeBrowser browser, YoutubeAccount account, string pageUrl, LoginPasswordPageV1 loginPasswordPage)
		{
			loginPasswordPage.SetPassword(account.Credentials.Password);
			loginPasswordPage.Submit();
			for (;; Thread.Sleep(500))
			{
				if (pageUrl != browser.Browser.WebView.Url)
				{
					break;
				}
			}
		}

		private LoginState GetLoginState()
		{
			if (LoginProxyErrorV1.Detect(_browser))
			{
				return new LoginProxyErrorV1(_browser.Browser);
			}

			if (LoginErrorPageV1.Detect(_browser.Browser))
			{
				return new LoginErrorPageV1(_browser.Browser);
			}

			if (LoginEmailPageV1.Detect(_browser))
			{
				return new LoginEmailPageV1(_browser.Browser);
			}

			if (LoginCaptchaPageV1.Detect(_browser))
			{
				return new LoginCaptchaPageV1(_browser.Browser);
			}

			if (LoginPasswordPageV1.Detect(_browser))
			{
				return new LoginPasswordPageV1(_browser.Browser);
			}

			if (LoginSelectRecoveryPageV1.Detect(_browser))
			{
				return new LoginSelectRecoveryPageV1(_browser.Browser);
			}

			if (LoginSubmitRecoveryPageV1.Detect(_browser))
			{
				return new LoginSubmitRecoveryPageV1(_browser.Browser);
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

			if (LoginYoutubeHomeStateV1.Detect(_browser))
			{
				return new LoginYoutubeHomeStateV1(_browser.Browser);
			}

			return null;
		}

		private LoadSignInResult LoadSignIn(YoutubeBrowser browser)
		{
			browser.Browser.WebView.CustomUserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_1_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B206 Safari/7534.48.3";
			browser.Browser.WebView.LoadUrlAndWait("https://accounts.google.com/ServiceLogin?sacu=1#identifier&hl=en");

			if (browser.Browser.WebView.Url.Contains("general-light") || browser.Browser.WebView.Url.Contains("youtube.com"))
			{
				return new LoadSignInResult(LoadSignInResultCode.AlreadyLoggedIn);
			}

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