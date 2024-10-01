using DefaultCoreLibrary.Core;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Commands.DeleteIndividualById;

public record DeleteIndividualByIdCommand(Id Id) : IRequest<Result<bool>>;
