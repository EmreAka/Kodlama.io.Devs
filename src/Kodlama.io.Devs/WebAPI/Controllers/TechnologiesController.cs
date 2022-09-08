using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Queries.GetListTechnology;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnologiesController : Controller
{
    private readonly IMediator _mediator;

    public TechnologiesController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]CreateTechnologyCommand createTechnologyCommand)
    {
        var result = await _mediator.Send(createTechnologyCommand);
        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand updateTechnologyCommand)
    {
        var result = await _mediator.Send(updateTechnologyCommand);
        return Ok(result);
    }

    [HttpPost("{Id}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteTechnologyCommand deleteTechnologyCommand)
    {
        var result = await _mediator.Send(deleteTechnologyCommand);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest)
    {
        GetTechnologyListQuery getTechnologyListQuery = new GetTechnologyListQuery(){PageRequest = pageRequest};

        var result = await _mediator.Send(getTechnologyListQuery);

        return Ok(result);
    }
}