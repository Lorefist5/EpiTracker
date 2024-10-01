using DefaultCoreLibrary.Core;
using EpiTracker.Application.Common.Extensions;
using EpiTracker.Domain.Features.Individuals.Dtos;
using EpiTracker.Domain.Features.Individuals.Interfaces;
using FluentValidation;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Commands.CreateIndividual;

public class CreateIndividualCommandHandler : IRequestHandler<CreateIndividualCommand, Result<int>>
{
    private readonly IIndividualRepository _individualRepository;
    private readonly IValidator<CreateIndividualDto> _validator;
    public CreateIndividualCommandHandler(IIndividualRepository individualRepository, IValidator<CreateIndividualDto> validator)
    {
        _individualRepository = individualRepository;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.CreationRequest, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.Errors.ToDomainErrors();


        return await _individualRepository.CreateIndividualAsync(request.CreationRequest, cancellationToken);
    }
}
