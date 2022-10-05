using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Commands.LoginDeveloper;
using Application.Features.Developers.Queries.GetListUser;
using Core.Domain.Entities;
using Core.Security.Dtos;
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
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            CreateDeveloperCommand createDeveloperCommand = new()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };

            var result = await _mediator.Send(createDeveloperCommand);
            
            SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand loginDeveloperCommand)
        {
            var result = await _mediator.Send(loginDeveloperCommand);

            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery]GetUserListQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        
        private string? GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) 
                return Request.Headers["X-Forwarded-For"];
            
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
        
        private void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };
        
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
