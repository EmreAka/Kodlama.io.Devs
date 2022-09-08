using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;
using Core.Application.Requests;
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
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateGitHubProfileCommand updateGitHubProfileCommand)
    {
        var result = await _mediator.Send(updateGitHubProfileCommand);

        return Ok(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete([FromRoute]DeleteGitHubProfileCommand deleteGitHubProfileCommand)
    {
        var result = await _mediator.Send(deleteGitHubProfileCommand);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest)
    {
        GetListGitHubProfileQuery getListGitHubProfileQuery = new() { PageRequest = pageRequest };

        var result = await _mediator.Send(getListGitHubProfileQuery);

        return Ok(result);
    }
}
