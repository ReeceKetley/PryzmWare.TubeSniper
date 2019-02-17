﻿using FluentValidation;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Presentation.Wpf.ViewModels.Auth;

namespace TubeSniper.Presentation.Wpf.Validators.Auth
{
	public class AuthViewModelValidator : AbstractValidator<AuthViewModel>
	{
		public AuthViewModelValidator()
		{
			RuleFor(x => x.LicenseKey)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty.")
				.Must(x => LicenseKey.TryCreate(x, out _)).WithMessage("Must be a valid License key.");
		}
	}
}