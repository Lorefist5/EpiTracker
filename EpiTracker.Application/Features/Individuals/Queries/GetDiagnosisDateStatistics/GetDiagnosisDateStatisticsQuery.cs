using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetDiagnosisDateStatistics;

public record GetDiagnosisDateStatisticsQuery : IRequest<GetDiagnosisDateStatisticsResponse>;
