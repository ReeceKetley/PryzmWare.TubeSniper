using System;
using System.Threading;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Extensions;
using TubeSniper.YouTubeBot.States;
using TubeSniper.YouTubeBot.States.V0;

namespace TubeSniper.YouTubeBot.Youtube
{
	class YoutubeLoginBotV1
	{
		private readonly YoutubeAccount _account;
		private readonly YoutubeBot _bot;

		public YoutubeLoginBotV1(YoutubeBot bot, YoutubeAccount account)
		{
			_bot = bot;
			_account = account;
		}

		public LoginResult Run()
		{
			return Login(_bot, _account);
		}

		public LoginResult Login(YoutubeBot bot, YoutubeAccount account)
		{
			var loadSignInResult = LoadSignIn(bot);
			if (loadSignInResult.Code == LoadSignInResultCode.AlreadyLoggedIn)
			{
				return new LoginResult(LoginResultCode.Success);
			}

			if (loadSignInResult.Code != LoadSignInResultCode.Success)
			{
				return new LoginResult(LoginResultCode.HttpError);
			}

			for (;; Thread.Sleep(750))
			{
				while (bot.Browser.WebView.IsLoading && !bot.Browser.WebView.CanEvalScript)
				{
					Thread.Sleep(50);
				}

				var page = GetLoginState();
				var pageUrl = bot.Browser.WebView.Url;
				switch (page)
				{
					case LoginCreateChannelPageV1 loginCreateChannelPageV1:
					{
						loginCreateChannelPageV1.Submit();
						break;
					}
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
						var type = LoginErrorPageV1.GetErrorType(bot.Browser);
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
						HandleLoginPasswordState(bot, account, pageUrl, loginPasswordPage);
						break;
					}
					case LoginUpgradeBrowserPageV1 loginUpgradeBrowserPage:
					{
						HandleLoginUpgradeBrowser(bot, loginUpgradeBrowserPage);
						break;
					}
					case LoginCaptchaPageV1 loginCaptchaPage:
					{
						HandleLoginCaptchaState(account, loginCaptchaPage);
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
						if (bot.Browser.WebView.Url.Contains("youtube.com/"))
						{
							return new LoginResult(LoginResultCode.Success);
						}

						continue;
					}
				}
			}
		}

		private void HandleLoginUpgradeBrowser(YoutubeBot bot, LoginUpgradeBrowserPageV1 loginUpgradeBrowserPage)
		{
			loginUpgradeBrowserPage.SetUserAgent(bot);
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

		private void HandleLoginCaptchaState(YoutubeAccount account, LoginCaptchaPageV1 loginCaptchaPage)
		{
			loginCaptchaPage.SetPassword(account.Credentials.Password);
			var img = loginCaptchaPage.GetImageUrl();
			var result = _bot.SolveCaptcha(new Uri(img));
			if (result == null)
			{
				throw new Exception();
			}
			loginCaptchaPage.SetToken(result);
			loginCaptchaPage.Submit();
		}

		private static void HandleLoginEmailState(YoutubeAccount account, LoginEmailPageV1 loginEmailPage)
		{
			loginEmailPage.SubmitEmail(account.Credentials.Email);
		}

		private static void HandleLoginPasswordState(YoutubeBot browser, YoutubeAccount account, string pageUrl, LoginPasswordPageV1 loginPasswordPage)
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
			if (LoginCreateChannelPageV1.Detect(_bot))
			{
				return new LoginCreateChannelPageV1(_bot.Browser);
			}
			if (LoginProxyErrorV1.Detect(_bot))
			{
				return new LoginProxyErrorV1(_bot.Browser);
			}

			if (LoginErrorPageV1.Detect(_bot.Browser))
			{
				return new LoginErrorPageV1(_bot.Browser);
			}

			if (LoginEmailPageV1.Detect(_bot))
			{
				return new LoginEmailPageV1(_bot.Browser);
			}

			if (LoginCaptchaPageV1.Detect(_bot))
			{
				return new LoginCaptchaPageV1(_bot.Browser);
			}

			if (LoginPasswordPageV1.Detect(_bot))
			{
				return new LoginPasswordPageV1(_bot.Browser);
			}

			if (LoginSelectRecoveryPageV1.Detect(_bot))
			{
				return new LoginSelectRecoveryPageV1(_bot.Browser);
			}

			if (LoginSubmitRecoveryPageV1.Detect(_bot))
			{
				return new LoginSubmitRecoveryPageV1(_bot.Browser);
			}

			if (LoginAccountSuspended.Detect(_bot))
			{
				return new LoginAccountSuspended(_bot.Browser);
			}

			if (LoginAccountNotFound.Detect(_bot))
			{
				return new LoginAccountNotFound(_bot.Browser);
			}

			if (LoginUpgradeBrowserPageV1.Detect(_bot))
			{
				return new LoginUpgradeBrowserPageV1(_bot.Browser);
			}

			if (LoginYoutubeHomeStateV1.Detect(_bot))
			{
				return new LoginYoutubeHomeStateV1(_bot.Browser);
			}

			return null;
		}

		private LoadSignInResult LoadSignIn(YoutubeBot browser)
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