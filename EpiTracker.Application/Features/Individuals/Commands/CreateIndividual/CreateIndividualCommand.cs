using DefaultCoreLibrary.Core;
using EpiTracker.Domain.Features.Individuals.Dtos;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Commands.CreateIndividual;

public record CreateIndividualCommand(CreateIndividualDto CreationRequest) : IRequest<Result<int>>;
