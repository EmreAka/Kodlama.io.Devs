using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
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
    public async Task<IActionResult> Add([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
    {
        var result = await _mediator.Send(deleteOperationClaimCommand);

        return Ok(result);
    }
}