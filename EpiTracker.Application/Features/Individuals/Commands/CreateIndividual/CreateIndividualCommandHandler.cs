using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Domain.Features.Individuals.Dtos;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EpiTracker.Application.Features.Individuals.Commands.CreateIndividual;

public class CreateIndividualCommandHandler : IRequestHandler<CreateIndividualCommand, Result<int>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<CreateIndividualDto> _dtoValidator;
    private readonly ILogger<CreateIndividualCommandHandler> _logger;

    public CreateIndividualCommandHandler(IIndividualRepository individualRepository, IValidator<CreateIndividualDto> validator, ILogger<CreateIndividualCommandHandler> logger)
    {
        _individualRepository = individualRepository;
        _dtoValidator = validator;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateIndividualCommand for individual: {@Individual}", request.CreationRequest);
        var dtoValidationResult = await _dtoValidator.ValidateAsync(request.CreationRequest, cancellationToken);
        if (dtoValidationResult.IsValid is false)
        {
            _logger.LogWarning("Validation failed for individual: {@Errors}", dtoValidationResult.Errors); // Log validation failures
            return dtoValidationResult.Errors.ToDomainErrors();
        }

        // Log before creating the individual
        _logger.LogInformation("Creating individual: {@Individual}", request.CreationRequest);
        var individualCreationResult = await _individualRepository.CreateIndividualAsync(request.CreationRequest, cancellationToken);
        if (individualCreationResult.IsFailure)
        {
            _logger.LogError("Failed to create individual: {@Errors}", individualCreationResult.Errors); // Log creation failures
            return individualCreationResult.Errors.ToList();
        }
        return individualCreationResult.Value;
    }
}
