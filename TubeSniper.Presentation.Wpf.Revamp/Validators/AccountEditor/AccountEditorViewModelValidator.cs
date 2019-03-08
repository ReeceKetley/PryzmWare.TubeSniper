using FluentValidation;
using TubeSniper.Core.Domain.Auth;
using TubeSniper.Presentation.Wpf.Accounts.ViewModels;

namespace TubeSniper.Presentation.Wpf.Validators.AccountEditor
{
	public class AccountEditorViewModelValidator : AbstractValidator<AccountEditorViewModel>
	{
		public AccountEditorViewModelValidator()
		{
			RuleFor(x => x.Email)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty.")
				.EmailAddress().WithMessage("Must be a valid email.");

			RuleFor(x => x.Password)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty.");

			RuleFor(x => x.RecoveryEmail)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty.")
				.EmailAddress().WithMessage("Must be a valid email.");
		}
	}
}

