using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using FluentValidation;

namespace EpiTracker.Application.Features.Individuals.ValueObjects.Validators;

public class IdValidator : AbstractValidator<Id>
{
    public IdValidator()
    {
        RuleFor(p => p.Value)
            .GreaterThan(0).WithMessage("{PropertyName} must be higher than 0.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}
