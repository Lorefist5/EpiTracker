using EpiTracker.Application.Features.Individuals.Interfaces;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetMostCommonSymptoms;

public class GetMostCommonSymptomsQueryHandler : IRequestHandler<GetMostCommonSymptomsQuery, GetMostCommonSymptomsQueryResponse>
{
    private readonly IIndividualStatisticsService _individualStatisticsService;

    public GetMostCommonSymptomsQueryHandler(IIndividualStatisticsService individualStatisticsService)
    {
        _individualStatisticsService = individualStatisticsService;
    }

    public async Task<GetMostCommonSymptomsQueryResponse> Handle(GetMostCommonSymptomsQuery request, CancellationToken cancellationToken)
    {
        var mostCommonSymptoms = await _individualStatisticsService.GetMostCommonSymptomsAsync();

        return new GetMostCommonSymptomsQueryResponse(mostCommonSymptoms);
    }
}
