using EpiTracker.Application.Features.Individuals.Queries.GetDiagnosisDateStatistics;
using EpiTracker.Application.Features.Individuals.Queries.GetMostCommonSymptoms;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EpiTracker.Api.Controllers.Individuals.Statistics;

[Route("api/[controller]")]
[ApiController]
public class IndividualStatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndividualStatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetDiagnosisDateStatistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetDiagnosisDateStatisticsResponse>> GetDiagnosisDateStatistics()
    {
        var query = new GetDiagnosisDateStatisticsQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("GetMostCommonSymptoms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GetMostCommonSymptomsQueryResponse>> GetMostCommonSymptoms()
    {
        var query = new GetMostCommonSymptomsQuery();
        var response = await _mediator.Send(query);

        return Ok(response);
    }
}
