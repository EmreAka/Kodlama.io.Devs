using Application.Features.Developers.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Developers.Commands.LoginDeveloper;

public class LoginDeveloperCommand : IRequest<TokenDto>
{
    public string Email { get; set; }
    public string Password { get; set; }

    public class LoginUserCommandHandler : IRequestHandler<LoginDeveloperCommand, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper)
            => (_mapper, _userRepository, _tokenHelper) = (mapper, userRepository, tokenHelper);

        public async Task<TokenDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(u => u.Email.ToLower() == request.Email.ToLower());

            //business rule

            var result = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);

            if (!result)
                throw new Exception("Wrong Credentials");

            var token = _tokenHelper.CreateToken(user, new List<OperationClaim>());

            return new() { Token = token.Token, Expiration = token.Expiration };
        }
    }
}
