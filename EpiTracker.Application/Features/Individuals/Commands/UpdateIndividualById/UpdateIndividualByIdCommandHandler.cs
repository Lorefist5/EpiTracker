using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Dtos;
using EpiTracker.Domain.Features.Individuals.Errors;
using EpiTracker.Domain.Features.Individuals.Exceptions;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Commands.UpdateIndividualById;

public class UpdateIndividualByIdCommandHandler : IRequestHandler<UpdateIndividualByIdCommand, HttpResult<bool>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<Id> _idValidator;
    private readonly IValidator<UpdateIndividualDto> _dtoValidator;
    public UpdateIndividualByIdCommandHandler(IIndividualRepository individualRepository, IValidator<Id> idValidator, IValidator<UpdateIndividualDto> dtoValidator)
    {
        _individualRepository = individualRepository;
        _idValidator = idValidator;
        _dtoValidator = dtoValidator;
    }

    public async Task<HttpResult<bool>> Handle(UpdateIndividualByIdCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _idValidator.ValidateAsync(request.Id);
        if (validationResult.IsValid is false)
            return validationResult.Errors.ToHttpDomainErrors();

        var dtoValidationResult = await _dtoValidator.ValidateAsync(request.UpdateRequest);
        if (dtoValidationResult.IsValid is false)
            return dtoValidationResult.Errors.ToHttpDomainErrors();

        var individualUpdateActionResult = await _individualRepository.UpdateIndividualAsync(request.Id.Value, request.UpdateRequest, cancellationToken);
        if(individualUpdateActionResult.IsFailure)
            return individualUpdateActionResult.Error;

        return individualUpdateActionResult.Value;
    }       
}
