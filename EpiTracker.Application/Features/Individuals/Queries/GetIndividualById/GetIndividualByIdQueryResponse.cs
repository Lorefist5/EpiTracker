using EpiTracker.Domain.Features.Individuals.Dtos;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividualById;

public record GetIndividualByIdQueryResponse(GetIndividualDto Individual);