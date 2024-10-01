using EpiTracker.Domain.Features.Individuals.Interfaces;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividuals;

public class GetIndividualsQueryHandler : IRequestHandler<GetIndividualsQuery, GetIndividualsQueryResponse>
{
    private readonly IIndividualRepository _individualRepository;

    public GetIndividualsQueryHandler(IIndividualRepository individualRepository)
    {
        _individualRepository = individualRepository;
    }

    public async Task<GetIndividualsQueryResponse> Handle(GetIndividualsQuery request, CancellationToken cancellationToken)
    {
        var fetchAllIndividualsResult = await _individualRepository.GetIndividualsAsync(cancellationToken);
        
        return new GetIndividualsQueryResponse(fetchAllIndividualsResult);
    }
}
