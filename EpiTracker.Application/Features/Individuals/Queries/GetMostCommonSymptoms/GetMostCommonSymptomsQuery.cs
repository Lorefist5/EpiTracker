using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetMostCommonSymptoms;

public record GetMostCommonSymptomsQuery : IRequest<GetMostCommonSymptomsQueryResponse>;