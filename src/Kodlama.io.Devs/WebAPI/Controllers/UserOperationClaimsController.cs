using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Queries.GetByUserIdUserOperationClaim;
using Application.Features.UserOperationClaims.Queries.GetListUserOperationClaim;
using Core.Application.Requests;
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
    
    [HttpDelete()]
    public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
    {
        var result = await _mediator.Send(deleteUserOperationClaimCommand);

        return Ok(result);
    }
    
    [HttpGet()]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserOperationClaimQuery getListUserOperationClaimQuery = new()
        {
            PageRequest = pageRequest
        };
        
        var result = await _mediator.Send(getListUserOperationClaimQuery);

        return Ok(result);
    }
    
    [HttpGet("/{userId}")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromRoute]int userId)
    {
        GetListByUserIdUserOperationClaimQuery getListByUserIdUserOperationClaimQuery = new()
        {
            PageRequest = pageRequest,
            UserId = userId
        };
        
        var result = await _mediator.Send(getListByUserIdUserOperationClaimQuery);

        return Ok(result);
    }
}