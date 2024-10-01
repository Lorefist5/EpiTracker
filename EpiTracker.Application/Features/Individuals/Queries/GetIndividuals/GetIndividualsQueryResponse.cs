using EpiTracker.Domain.Features.Individuals.Dtos;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividuals;

public record GetIndividualsQueryResponse(IEnumerable<GetIndividualDto> Individuals);
