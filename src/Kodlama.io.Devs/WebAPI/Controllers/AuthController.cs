using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.LoginUser;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var result = await _mediator.Send(loginUserCommand);

            return Ok(result);
        }
    }
}
