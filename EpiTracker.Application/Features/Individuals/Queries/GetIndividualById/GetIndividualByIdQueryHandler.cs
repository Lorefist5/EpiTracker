using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Errors;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividualById;
public class GetIndividualByIdQueryHandler : IRequestHandler<GetIndividualByIdQuery, HttpResult<GetIndividualByIdQueryResponse>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<Id> _idValidator;
    private readonly ILogger<GetIndividualByIdQueryHandler> _logger;

    public GetIndividualByIdQueryHandler(IIndividualRepository individualRepository, IValidator<Id> requestValidator, ILogger<GetIndividualByIdQueryHandler> logger)
    {
        _individualRepository = individualRepository;
        _idValidator = requestValidator;
        _logger = logger;
    }

    public async Task<HttpResult<GetIndividualByIdQueryResponse>> Handle(GetIndividualByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating ID for fetching individual.");
        var validationResult = await _idValidator.ValidateAsync(request.Id);
        if (validationResult.IsValid is false)
        {
            _logger.LogWarning("ID validation failed: {Errors}", validationResult.Errors);
            return validationResult.Errors.ToHttpDomainErrors();
        }

        _logger.LogInformation("Fetching individual with ID {Id}.", request.Id.Value);
        var individualFetchByIdResult = await _individualRepository.GetIndividualByIdAsync(request.Id.Value, cancellationToken);
        if (individualFetchByIdResult is null)
        {
            _logger.LogWarning("Individual with ID {Id} not found.", request.Id.Value);
            return IndividualErrors.IndividualNotFound(request.Id.Value);
        }

        _logger.LogInformation("Successfully fetched individual with ID {Id}.", request.Id.Value);
        return new GetIndividualByIdQueryResponse(individualFetchByIdResult);
    }
}