﻿namespace TubeSniper.Core.Domain.Models.States.V1
{
	enum LoginFormErrorEnums
	{
		PasswordInvalid,
		AccountNotFound,
		InvalidCaptcha,
		AccountSusspended,
		UnkownError,
		SubmitRecoveryFail
	}
}
