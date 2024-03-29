﻿using System;
using System.Threading;
using TubeSniper.YouTubeBot.Auth;
using TubeSniper.YouTubeBot.Extensions;

namespace TubeSniper.YouTubeBot.States
{
	class LoginErrorPageV1 : LoginState
	{
		public LoginErrorPageV1(VirtualBrowser.VirtualBrowser browser) : base(browser)
		{
		}

		public static LoginFormErrorEnums GetErrorType(VirtualBrowser.VirtualBrowser browser)
		{
			while (!browser.WebView.CanEvalScript)
			{
				Thread.Sleep(50);
			}

			if (browser.WebView.ElementExists("#error"))
			{
				return LoginFormErrorEnums.SubmitRecoveryFail;
			}

			var errorType = browser.WebView.EvalScript("$(\".form-error\").attr(\"id\")").ToString();
			Console.WriteLine("Error Type: " + errorType);
			switch (errorType)
			{
				case "Passwd":
					return LoginFormErrorEnums.PasswordInvalid;
				case "password":
					return LoginFormErrorEnums.PasswordInvalid;
				case "Email":
					return LoginFormErrorEnums.AccountNotFound;
				case "username":
					return LoginFormErrorEnums.AccountNotFound;
				default:
					break;
			}

			return LoginFormErrorEnums.UnkownError;
		}

		public static bool Detect(VirtualBrowser.VirtualBrowser browser)
		{
			while (!browser.WebView.CanEvalScript && !browser.WebView.IsReady)
			{
				Thread.Sleep(50);
			}

			if (browser.WebView.ElementExists(".form-error") || browser.WebView.ElementExists("#error"))
			{
				Console.WriteLine("Form Error Detected");
				return true;
			}

			return false;
		}
	}
}