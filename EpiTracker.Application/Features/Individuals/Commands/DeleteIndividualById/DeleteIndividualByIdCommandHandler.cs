using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Commands.DeleteIndividualById;

public class DeleteIndividualByIdCommandHandler : IRequestHandler<DeleteIndividualByIdCommand, HttpResult<bool>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<Id> _idValidator;
    public DeleteIndividualByIdCommandHandler(IIndividualRepository individualRepository, IValidator<Id> idValidator)
    {
        _individualRepository = individualRepository;
        _idValidator = idValidator;
    }
    public async Task<HttpResult<bool>> Handle(DeleteIndividualByIdCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _idValidator.ValidateAsync(request.Id);
        if (validationResult.IsValid is false)
            return validationResult.Errors.ToHttpDomainErrors();

        var individualDeleteResult = await _individualRepository.DeleteIndividualAsync(request.Id.Value, cancellationToken);
        if(individualDeleteResult.IsFailure)
            return individualDeleteResult;

        return individualDeleteResult;
    }
}
