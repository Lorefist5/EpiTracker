using EpiTracker.Application.Features.Individuals.Queries.GetDiagnosisDateStatistics;
using EpiTracker.Application.Features.Individuals.Queries.GetMostCommonSymptoms;
using MediatR;

namespace EpiTracker.Aspire.ApiService.Features.Individuals.Apis;

public static class IndividualStatisticsApi
{
    public static IEndpointRouteBuilder MapIndividualStatisticsApis(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/individual/statistics");

        group.MapGet("/GetDiagnosisDateStatistics", async (IMediator mediator) =>
        {
            var query = new GetDiagnosisDateStatisticsQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        group.MapGet("/GetMostCommonSymptoms", async (IMediator mediator) =>
        {
            var query = new GetMostCommonSymptomsQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        return app;
    }
}