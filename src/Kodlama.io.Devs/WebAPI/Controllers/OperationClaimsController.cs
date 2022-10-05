using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : Controller
{
    private readonly IMediator _mediator;

    public OperationClaimsController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost()]
    public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
    {
        var result = await _mediator.Send(createOperationClaimCommand);

        return Created("", result);
    }
    
    [HttpDelete()]
    public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
    {
        var result = await _mediator.Send(deleteOperationClaimCommand);

        return Ok(result);
    }
    
    [HttpPut()]
    public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
    {
        var result = await _mediator.Send(updateOperationClaimCommand);

        return Ok(result);
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetAll([FromQuery] PageRequest pageRequest)
    {
        var query = new GetListOperationClaimQuery()
        {
            PageRequest = pageRequest
        };
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}