﻿namespace TubeSniper.YouTubeBot.Auth
{
    public class LoadSignInResult
    {
        public LoadSignInResultCode Code { get; }

	    public LoadSignInResult()
        {
            Code = LoadSignInResultCode.Success;
        }

        public LoadSignInResult(LoadSignInResultCode httpError)
        {
            Code = httpError;
        }
    }
}