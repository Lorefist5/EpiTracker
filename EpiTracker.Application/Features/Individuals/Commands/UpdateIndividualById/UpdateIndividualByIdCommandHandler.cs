using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Dtos;
using EpiTracker.Domain.Features.Individuals.Errors;
using EpiTracker.Domain.Features.Individuals.Exceptions;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EpiTracker.Application.Features.Individuals.Commands.UpdateIndividualById;
public class UpdateIndividualByIdCommandHandler : IRequestHandler<UpdateIndividualByIdCommand, HttpResult<bool>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<Id> _idValidator;
    private readonly IValidator<UpdateIndividualDto> _dtoValidator;
    private readonly ILogger<UpdateIndividualByIdCommandHandler> _logger;

    public UpdateIndividualByIdCommandHandler(IIndividualRepository individualRepository, IValidator<Id> idValidator, IValidator<UpdateIndividualDto> dtoValidator, ILogger<UpdateIndividualByIdCommandHandler> logger)
    {
        _individualRepository = individualRepository;
        _idValidator = idValidator;
        _dtoValidator = dtoValidator;
        _logger = logger;
    }

    public async Task<HttpResult<bool>> Handle(UpdateIndividualByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating ID for update.");
        var validationResult = await _idValidator.ValidateAsync(request.Id);
        if (validationResult.IsValid is false)
        {
            _logger.LogWarning("ID validation failed: {Errors}", validationResult.Errors);
            return validationResult.Errors.ToHttpDomainErrors();
        }

        _logger.LogInformation("Validating update request.");
        var dtoValidationResult = await _dtoValidator.ValidateAsync(request.UpdateRequest);
        if (dtoValidationResult.IsValid is false)
        {
            _logger.LogWarning("Update request validation failed: {Errors}", dtoValidationResult.Errors);
            return dtoValidationResult.Errors.ToHttpDomainErrors();
        }

        _logger.LogInformation("Updating individual with ID {Id}.", request.Id.Value);
        var individualUpdateActionResult = await _individualRepository.UpdateIndividualAsync(request.Id.Value, request.UpdateRequest, cancellationToken);
        if (individualUpdateActionResult.IsFailure)
        {
            _logger.LogError("Failed to update individual with ID {Id}.", request.Id.Value);
            return individualUpdateActionResult.Error;
        }

        _logger.LogInformation("Successfully updated individual with ID {Id}.", request.Id.Value);
        return individualUpdateActionResult.Value;
    }
}