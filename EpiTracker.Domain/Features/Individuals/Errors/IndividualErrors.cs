using DefaultCoreLibrary.Core;

namespace EpiTracker.Domain.Features.Individuals.Errors;

public static class IndividualErrors
{
    public static Error IndividualNotFound(int individualId) =>
        new Error("Individual.NotFound", $"Individual with Id {individualId} was not found.");

}
