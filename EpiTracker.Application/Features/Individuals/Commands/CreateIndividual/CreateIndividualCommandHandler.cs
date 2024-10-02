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
    private readonly IValidator<CreateIndividualDto> _validator;
    private readonly ILogger<CreateIndividualCommandHandler> _logger;

    public CreateIndividualCommandHandler(IIndividualRepository individualRepository, IValidator<CreateIndividualDto> validator, ILogger<CreateIndividualCommandHandler> logger)
    {
        _individualRepository = individualRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateIndividualCommand for individual: {@Individual}", request.CreationRequest);
        var validationResult = await _validator.ValidateAsync(request.CreationRequest, cancellationToken);
        if (validationResult.IsValid is false)
        {
            _logger.LogWarning("Validation failed for individual: {@Errors}", validationResult.Errors); // Log validation failures
            return validationResult.Errors.ToDomainErrors();
        }

        // Log before creating the individual
        _logger.LogInformation("Creating individual: {@Individual}", request.CreationRequest);

        return await _individualRepository.CreateIndividualAsync(request.CreationRequest, cancellationToken);
    }
}
