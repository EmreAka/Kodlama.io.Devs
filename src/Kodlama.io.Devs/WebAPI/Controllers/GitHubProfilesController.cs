using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GitHubProfilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GitHubProfilesController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("add")]
    public async Task<IActionResult> Add(CreateGitHubProfileCommand createGitHubProfileCommand)
    {
        var result = await _mediator.Send(createGitHubProfileCommand);

        return Ok(result);
    }
    
    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateGitHubProfileCommand updateGitHubProfileCommand)
    {
        var result = await _mediator.Send(updateGitHubProfileCommand);

        return Ok(result);
    }
}
