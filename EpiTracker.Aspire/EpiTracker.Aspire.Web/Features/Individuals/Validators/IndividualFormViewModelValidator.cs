using EpiTracker.Aspire.Web.Features.Individuals.ViewModels;
using FluentValidation;

namespace EpiTracker.Aspire.Web.Features.Individuals.Validators;
 
public class IndividualFormViewModelValidator : AbstractValidator<IndividualFormViewModel>
{
    public IndividualFormViewModelValidator()
    {
        // Validate Name
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

        // Validate Age
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0).WithMessage("Age must be at least 0.")
            .LessThanOrEqualTo(120).WithMessage("Age must be less than or equal to 120.");

        // Validate Symptoms
        RuleFor(x => x.Symptoms)
            .NotEmpty().WithMessage("At least one symptom must be provided.")
            .ForEach(symptom =>
            {
                symptom.NotEmpty().WithMessage("Symptom cannot be empty.")
                       .Length(1, 50).WithMessage("Each symptom must be between 1 and 50 characters.");
            });

        // Validate DateDiagnosed (if it has any)
        RuleFor(x => x.DateDiagnosed)
            .LessThanOrEqualTo(DateTime.Today).When(x => x.DateDiagnosed.HasValue)
            .WithMessage("Date diagnosed cannot be in the future.");
    }
}
