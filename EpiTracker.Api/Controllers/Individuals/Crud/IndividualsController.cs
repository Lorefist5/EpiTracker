using EpiTracker.Application.Common.Extensions;
using EpiTracker.Application.Features.Individuals.Commands.CreateIndividual;
using EpiTracker.Application.Features.Individuals.Commands.DeleteIndividualById;
using EpiTracker.Application.Features.Individuals.Queries.GetIndividualById;
using EpiTracker.Application.Features.Individuals.Queries.GetIndividuals;
using EpiTracker.Application.Features.Individuals.ValueObjects.Objects;
using EpiTracker.Domain.Features.Individuals.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EpiTracker.Api.Controllers.Individuals.Crud;

[Route("api/[controller]")]
[ApiController]
public class IndividualsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndividualsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetIndividuals()
    {
        var query = new GetIndividualsQuery();
        var result = await _mediator.Send(query);

        return Ok(result.Individuals);
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetIndividualById(int userId)
    {
        var id = new Id(userId);
        var query = new GetIndividualByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure && result.Errors.ContainsNotFound())
            return NotFound(result.Errors.FetchNotFoundError());
        if (result.IsFailure)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    [HttpPost]
    public async Task<IActionResult> CreateIndividual([FromBody] CreateIndividualDto creationRequest)
    {
        var command = new CreateIndividualCommand(creationRequest);
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteIndividual(int userId)
    {
        var id = new Id(userId);
        var query = new DeleteIndividualByIdCommand(id);
        var result = await _mediator.Send(query);

        if (result.IsFailure && result.Errors.ContainsNotFound())
            return NotFound(result.Errors.FetchNotFoundError());
        if (result.IsFailure)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
