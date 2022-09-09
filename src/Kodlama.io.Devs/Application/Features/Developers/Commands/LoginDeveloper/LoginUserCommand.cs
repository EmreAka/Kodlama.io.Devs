using Application.Features.Developers.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Application.Features.Developers.Rules;
using Microsoft.EntityFrameworkCore;

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
        private readonly DeveloperBusinessRules _developerBusinessRules;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper,
         ITokenHelper tokenHelper, DeveloperBusinessRules developerBusinessRules)
            => (_mapper, _userRepository, _tokenHelper, _developerBusinessRules)
            = (mapper, userRepository, tokenHelper, developerBusinessRules);

        public async Task<TokenDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(
                u => u.Email.ToLower() == request.Email.ToLower(),
                include: m => m.Include(c => c.UserOperationClaims).ThenInclude(x => x.OperationClaim));

            var operationClaims = new List<OperationClaim>() { };

            foreach (var userOperationClaim in user.UserOperationClaims)
            {
                operationClaims.Add(userOperationClaim.OperationClaim);
            }

            _developerBusinessRules.UserShouldExist(user);

            _developerBusinessRules.UserCredentialsShouldMatch(request.Password, user.PasswordHash, user.PasswordSalt);

            AccessToken token = _tokenHelper.CreateToken(user, operationClaims);

            TokenDto tokenDto = _mapper.Map<TokenDto>(token);

            return tokenDto;
        }
    }
}
