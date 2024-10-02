using DefaultCoreLibrary.Core;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Dtos;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Commands.UpdateIndividualById;

public record UpdateIndividualByIdCommand(Id Id, UpdateIndividualDto UpdateRequest) : IRequest<HttpResult<bool>>;