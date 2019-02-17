using FluentValidation;
using TubeSniper.Presentation.Wpf.ViewModels.CampaignEditor;

namespace TubeSniper.Presentation.Wpf.Validators.CampaignEditor
{
	public class CampaignEditorViewModelValidator : AbstractValidator<CampaignEditorViewModel>
	{
		public CampaignEditorViewModelValidator()
		{
			RuleFor(x => x.Title)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty")
				.MaximumLength(64).WithMessage("Must not be longer than 64 characters");
			
			RuleFor(x => x.SearchKeyword)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty")
				.MaximumLength(64).WithMessage("Must not be longer than 512 characters");

			RuleFor(x => x.MaxResults)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty")
				.LessThan(1024).WithMessage("Must not be greater than 1024 videos");

			RuleFor(x => x.WorkersCount)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty")
				.LessThan(5).WithMessage("Must not be greater than 5 workers");

			RuleFor(x => x.Comment)
				.Cascade(CascadeMode.StopOnFirstFailure)
				.NotEmpty().WithMessage("Must not be empty")
				.MaximumLength(2056).WithMessage("Must not be longer than 2056 characters");

			/*//Console.WriteLine("Save Called");
			if (WorkersCount <= 0)
			{
				//Console.WriteLine("No workers selected");
				return;
			}

			if (MaxResults <= 0)
			{
				//Console.WriteLine("No max videos selected");
				return;
			}

			if (string.IsNullOrEmpty(CampaignTitle))
			{
				//Console.WriteLine("No campaign title");
				return;
			}

			if (string.IsNullOrEmpty(SearchKeyword))
			{
				//Console.WriteLine("No search keyword entered");
				return;
			}

			if (string.IsNullOrEmpty(Comment))
			{
				//Console.WriteLine("Comment is null");
				return;
			}

			if (SelectedAccounts.Count <= 0)
			{
				//Console.WriteLine("No accounts selected");
				return;
			}
*/
		}
	}
}
