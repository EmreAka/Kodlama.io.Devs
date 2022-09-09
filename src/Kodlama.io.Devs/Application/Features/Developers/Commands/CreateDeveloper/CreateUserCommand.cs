using Application.Features.Developers.Dtos;
using Application.Services.Repositories;
using Application.Features.Developers.Rules;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Developers.Commands.CreateDeveloper;

public class CreateDeveloperCommand : IRequest<TokenDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public class CreateUserCommandHandler : IRequestHandler<CreateDeveloperCommand, TokenDto>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly DeveloperBusinessRules _developerBusinessRules;

        public CreateUserCommandHandler(IMapper mapper, ITokenHelper tokenHelper, IDeveloperRepository developerRepository, DeveloperBusinessRules developerBusinessRules)
            => (_mapper, _tokenHelper, _developerRepository, _developerBusinessRules) = (mapper, tokenHelper, developerRepository, developerBusinessRules);

        public async Task<TokenDto> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _developerBusinessRules.EmailCanNotBeDuplicatedWhenInserted(request.Email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            Developer developer = _mapper.Map<Developer>(request);
            developer.PasswordHash = passwordHash;
            developer.PasswordSalt = passwordSalt;

            var createdDeveloper = await _developerRepository.AddAsync(developer);

            var token = _tokenHelper.CreateToken(developer, new List<OperationClaim>());

            return new() { Token = token.Token, Expiration = token.Expiration };
        }
    }
}
