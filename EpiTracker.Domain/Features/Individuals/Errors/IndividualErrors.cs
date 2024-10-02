using DefaultCoreLibrary.Core;
using System.Net;

namespace EpiTracker.Domain.Features.Individuals.Errors;

public static class IndividualErrors
{
    public static HttpResultError IndividualNotFound(int individualId) =>
        new HttpResultError("Individual.NotFound", $"Individual with Id {individualId} was not found.", HttpStatusCode.NotFound);

}
