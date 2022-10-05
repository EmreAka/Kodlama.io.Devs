using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : Controller
{
    private readonly IMediator _mediator;

    public UserOperationClaimsController(IMediator mediator)
        => _mediator = mediator;
    
    [HttpPost()]
    public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
    {
        var result = await _mediator.Send(createUserOperationClaimCommand);

        return Created("", result);
    }
}