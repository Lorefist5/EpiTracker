using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Errors;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividualById;

public class GetIndividualByIdQueryHandler : IRequestHandler<GetIndividualByIdQuery, HttpResult<GetIndividualByIdQueryResponse>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<Id> _idValidator;
    public GetIndividualByIdQueryHandler(IIndividualRepository individualRepository, IValidator<Id> requestValidator)
    {
        _individualRepository = individualRepository;
        _idValidator = requestValidator;
    }

    public async Task<HttpResult<GetIndividualByIdQueryResponse>> Handle(GetIndividualByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _idValidator.ValidateAsync(request.Id);
        if (validationResult.IsValid is false)
            return validationResult.Errors.ToHttpDomainErrors();

        var individualFetchByIdResult = await _individualRepository.GetIndividualByIdAsync(request.Id.Value, cancellationToken);
        if (individualFetchByIdResult is null)
            return IndividualErrors.IndividualNotFound(request.Id.Value);


        return new GetIndividualByIdQueryResponse(individualFetchByIdResult);
    }
}
