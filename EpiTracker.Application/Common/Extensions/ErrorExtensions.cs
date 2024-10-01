using DefaultCoreLibrary.Core;
using FluentValidation.Results;

namespace EpiTracker.Application.Common.Extensions;

public static class ErrorExtensions
{

    public static List<Error> ToDomainErrors(this IEnumerable<ValidationFailure> failures)
    {
        return failures
            .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
            .ToList();
    }
    public static bool ContainsNotFound(this IEnumerable<Error> errors) => 
        errors.Any(error => error.Code.ToLower().Contains("notfound"));
    public static Error? FetchNotFoundError(this IEnumerable<Error> errors) => 
        errors.FirstOrDefault(error => error.Code.ToLower().Contains("notfound"));
    

}
