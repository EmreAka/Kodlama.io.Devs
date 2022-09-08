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

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest)
    {
        GetTechnologyListQuery getTechnologyListQuery = new GetTechnologyListQuery(){PageRequest = pageRequest};

        var result = await _mediator.Send(getTechnologyListQuery);

        return Ok(result);
    }
}