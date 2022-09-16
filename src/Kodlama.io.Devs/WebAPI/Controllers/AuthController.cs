using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Commands.LoginDeveloper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] CreateDeveloperCommand createDeveloperCommand)
        {


            var result = await _mediator.Send(createDeveloperCommand); return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand loginDeveloperCommand)
        {
            var result = await _mediator.Send(loginDeveloperCommand);

            return Ok(result);
        }
    }
}
