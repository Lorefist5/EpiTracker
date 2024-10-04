using EpiTracker.Domain.Features.Individuals.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividuals;

public class GetIndividualsQueryHandler : IRequestHandler<GetIndividualsQuery, GetIndividualsQueryResponse>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly ILogger<GetIndividualsQueryHandler> _logger;

    public GetIndividualsQueryHandler(IIndividualRepository individualRepository, ILogger<GetIndividualsQueryHandler> logger)
    {
        _individualRepository = individualRepository;
        _logger = logger;
    }

    public async Task<GetIndividualsQueryResponse> Handle(GetIndividualsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all individuals.");
        var fetchAllIndividualsResult = await _individualRepository.GetIndividualsAsync(cancellationToken);
        _logger.LogInformation("Fetched {Count} individuals.", fetchAllIndividualsResult.Count());

        return new GetIndividualsQueryResponse(fetchAllIndividualsResult);
    }
}
