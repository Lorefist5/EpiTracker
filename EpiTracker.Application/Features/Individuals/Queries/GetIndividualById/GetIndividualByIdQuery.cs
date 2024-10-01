using DefaultCoreLibrary.Core;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Dtos;
using MediatR;

namespace EpiTracker.Application.Features.Individuals.Queries.GetIndividualById;

public record GetIndividualByIdQuery(Id Id) : IRequest<Result<GetIndividualByIdQueryResponse>>;