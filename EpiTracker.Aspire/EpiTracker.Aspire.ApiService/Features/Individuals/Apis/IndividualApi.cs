using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.Commands.CreateIndividual;
using EpiTracker.Application.Features.Individuals.Commands.DeleteIndividualById;
using EpiTracker.Application.Features.Individuals.Commands.UpdateIndividualById;
using EpiTracker.Application.Features.Individuals.Queries.GetIndividualById;
using EpiTracker.Application.Features.Individuals.Queries.GetIndividuals;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Dtos;
using MediatR;

namespace EpiTracker.Aspire.ApiService.Features.Individuals.Apis;
public static class IndividualApi
{
    public static IEndpointRouteBuilder MapIndividualApis(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/individuals");

        group.MapGet("/", async (IMediator mediator) =>
        {
            var query = new GetIndividualsQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result.Individuals);
        });

        group.MapGet("/{userId:int}", async (int userId, IMediator mediator) =>
        {
            var id = new Id(userId);
            var query = new GetIndividualByIdQuery(id);
            var result = await mediator.Send(query);

            if (result.IsFailure)
            {
                if (result.Errors.ContainsNotFound())
                    return Results.NotFound(result.Errors.FetchNotFoundError());

                return Results.BadRequest(result.Errors);
            }

            return Results.Ok(result.Value);
        });

        group.MapPost("/", async (CreateIndividualDto creationRequest, IMediator mediator, HttpContext http) =>
        {
            var command = new CreateIndividualCommand(creationRequest);
            var result = await mediator.Send(command);

            if (result.IsFailure)
                return Results.BadRequest(result.Errors);

            var createdIndividualId = result.Value;
            var locationUri = $"{http.Request.Scheme}://{http.Request.Host}/api/individuals/{createdIndividualId}";

            return Results.Created(locationUri, new { Id = createdIndividualId, creationRequest });
        });

        group.MapDelete("/{userId:int}", async (int userId, IMediator mediator) =>
        {
            var id = new Id(userId);
            var query = new DeleteIndividualByIdCommand(id);
            var result = await mediator.Send(query);

            if (result.IsFailure && result.Errors.ContainsNotFound())
                return Results.NotFound(result.Errors.FetchNotFoundError());

            if (result.IsFailure)
                return Results.BadRequest(result.Errors);

            return Results.Ok(result.Value);
        });

        group.MapPut("/{userId:int}", async (int userId, UpdateIndividualDto updateRequest, IMediator mediator) =>
        {
            var id = new Id(userId);
            var command = new UpdateIndividualByIdCommand(id, updateRequest);
            var result = await mediator.Send(command);

            if (result.IsFailure && result.Errors.ContainsNotFound())
                return Results.NotFound(result.Errors.FetchNotFoundError());

            if (result.IsFailure)
                return Results.BadRequest(result.Errors);

            return Results.Ok(result.Value);
        });

        return app;
    }
}