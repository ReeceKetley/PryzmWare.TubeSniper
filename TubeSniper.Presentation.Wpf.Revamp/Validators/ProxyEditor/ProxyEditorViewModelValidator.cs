using FluentValidation;
using TubeSniper.Presentation.Wpf.ViewModels.ProxyEditor;

namespace TubeSniper.Presentation.Wpf.Validators.ProxyEditor
{
	public class ProxyEditorViewModelValidator : AbstractValidator<ProxyEditorViewModel>
	{
		public ProxyEditorViewModelValidator()
		{
			RuleFor(x => x.ProxyString)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty")
				.Equal(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]):[\d]+$").WithMessage("Must match proxy:port format");

			RuleFor(x => x.ProxyUsername)
				.NotEmpty().When(x => !string.IsNullOrEmpty(x.ProxyPassword)).WithMessage("Must not be empty when using credentials");

			RuleFor(x => x.ProxyPassword)
				.NotEmpty().When(x => !string.IsNullOrEmpty(x.ProxyUsername)).WithMessage("Must not be empty when using credentials");
		}
	}
}
