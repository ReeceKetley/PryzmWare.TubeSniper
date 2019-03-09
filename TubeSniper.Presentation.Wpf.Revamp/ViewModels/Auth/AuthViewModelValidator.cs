using FluentValidation;
using TubeSniper.Domain.Auth;

namespace TubeSniper.Presentation.Wpf.ViewModels.Auth
{
	public class AuthViewModelValidator : AbstractValidator<AuthViewModel>
	{
		public AuthViewModelValidator()
		{
			RuleFor(x => x.LicenseKey)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty.")
				.Must(x => ProductKey.TryCreate(x, out _)).WithMessage("Must be a valid License key.");
		}
	}
}
