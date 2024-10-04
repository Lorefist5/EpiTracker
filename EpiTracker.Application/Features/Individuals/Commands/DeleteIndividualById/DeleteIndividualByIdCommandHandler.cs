using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EpiTracker.Application.Features.Individuals.Commands.DeleteIndividualById;

public class DeleteIndividualByIdCommandHandler : IRequestHandler<DeleteIndividualByIdCommand, HttpResult<bool>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<Id> _idValidator;
    private readonly ILogger<DeleteIndividualByIdCommandHandler> _logger;

    public DeleteIndividualByIdCommandHandler(IIndividualRepository individualRepository, IValidator<Id> idValidator, ILogger<DeleteIndividualByIdCommandHandler> logger)
    {
        _individualRepository = individualRepository;
        _idValidator = idValidator;
        _logger = logger;
    }

    public async Task<HttpResult<bool>> Handle(DeleteIndividualByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating ID for deletion.");
        var validationResult = await _idValidator.ValidateAsync(request.Id);
        if (validationResult.IsValid is false)
        {
            _logger.LogWarning("ID validation failed: {Errors}", validationResult.Errors);
            return validationResult.Errors.ToHttpDomainErrors();
        }

        _logger.LogInformation("Deleting individual with ID {Id}.", request.Id.Value);
        var individualDeleteResult = await _individualRepository.DeleteIndividualAsync(request.Id.Value, cancellationToken);
        if (individualDeleteResult.IsFailure)
        {
            _logger.LogError("Failed to delete individual with ID {Id}.", request.Id.Value);
            return individualDeleteResult;
        }

        _logger.LogInformation("Successfully deleted individual with ID {Id}.", request.Id.Value);
        return individualDeleteResult;
    }
}