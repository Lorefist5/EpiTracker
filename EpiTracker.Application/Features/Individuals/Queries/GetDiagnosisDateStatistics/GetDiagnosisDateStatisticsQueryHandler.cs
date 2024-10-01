using EpiTracker.Application.Features.Individuals.Interfaces;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetDiagnosisDateStatistics;

public class GetDiagnosisDateStatisticsQueryHandler : IRequestHandler<GetDiagnosisDateStatisticsQuery, GetDiagnosisDateStatisticsResponse>
{
    private readonly IIndividualStatisticsService _individualStatisticsService;

    public GetDiagnosisDateStatisticsQueryHandler(IIndividualStatisticsService individualStatisticsService)
    {
        _individualStatisticsService = individualStatisticsService;
    }

    public async Task<GetDiagnosisDateStatisticsResponse> Handle(GetDiagnosisDateStatisticsQuery request, CancellationToken cancellationToken)
    {
        var diagnosisDateStatistics = await _individualStatisticsService.GetDiagnosisDateStatisticsAsync();
        
        return new GetDiagnosisDateStatisticsResponse(diagnosisDateStatistics);
    }
}
