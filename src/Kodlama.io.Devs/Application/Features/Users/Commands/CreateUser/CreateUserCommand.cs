using Application.Features.Users.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<TokenDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, TokenDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper)
            => (_userRepository, _mapper, _tokenHelper) = (userRepository, mapper, tokenHelper);

        public async Task<TokenDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                AuthenticatorType = Core.Security.Enums.AuthenticatorType.Email,
                Status = true,
            };

            var createdUser = await _userRepository.AddAsync(user);

            var token = _tokenHelper.CreateToken(user, new List<OperationClaim>());

            return new() { Token = token.Token, Expiration = token.Expiration };
        }
    }
}
