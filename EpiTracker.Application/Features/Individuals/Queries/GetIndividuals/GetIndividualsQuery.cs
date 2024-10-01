using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividuals;

public record GetIndividualsQuery : IRequest<GetIndividualsQueryResponse>;
