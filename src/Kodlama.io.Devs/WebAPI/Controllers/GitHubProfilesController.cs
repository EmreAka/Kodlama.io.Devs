using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
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

    [HttpPost]
    public async Task<IActionResult> Add(CreateGitHubProfileCommand createGitHubProfileCommand)
    {
        var result = await _mediator.Send(createGitHubProfileCommand);

        return Ok(result);
    }
}
