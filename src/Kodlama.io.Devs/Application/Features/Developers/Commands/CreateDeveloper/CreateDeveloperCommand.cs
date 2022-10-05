using Application.Features.Developers.Dtos;
using Application.Services.Repositories;
using Application.Features.Developers.Rules;
using Application.Services.AuthService;
using AutoMapper;
using Core.Domain.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Core.Security.Dtos;

namespace Application.Features.Developers.Commands.CreateDeveloper;

/*public class CreateDeveloperCommand : UserForRegisterDto, IRequest<TokenDto>
{
    public class CreateUserCommandHandler : IRequestHandler<CreateDeveloperCommand, TokenDto>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly DeveloperBusinessRules _developerBusinessRules;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IMapper mapper, ITokenHelper tokenHelper,
            IDeveloperRepository developerRepository, DeveloperBusinessRules developerBusinessRules,
            IAuthService authService)
            => (_mapper, _tokenHelper, _developerRepository, _developerBusinessRules, _authService) =
                (mapper, tokenHelper, developerRepository, developerBusinessRules, authService);

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

            var createdToken = _mapper.Map<TokenDto>(token);

            return createdToken;
        }
    }
}*/

public class CreateDeveloperCommand : IRequest<RegisteredDto>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }
    public class CreateUserCommandHandler : IRequestHandler<CreateDeveloperCommand, RegisteredDto>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        private readonly DeveloperBusinessRules _developerBusinessRules;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IMapper mapper, ITokenHelper tokenHelper,
            IDeveloperRepository developerRepository, DeveloperBusinessRules developerBusinessRules,
            IAuthService authService)
            => (_mapper, _tokenHelper, _developerRepository, _developerBusinessRules, _authService) =
                (mapper, tokenHelper, developerRepository, developerBusinessRules, authService);

        public async Task<RegisteredDto> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
        {
            await _developerBusinessRules.EmailCanNotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);

            Developer developer = _mapper.Map<Developer>(request.UserForRegisterDto);
            developer.PasswordHash = passwordHash;
            developer.PasswordSalt = passwordSalt;

            var createdDeveloper = await _developerRepository.AddAsync(developer);

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdDeveloper);
            RefreshToken createdRefreshToken =
                await _authService.CreateRefreshToken(createdDeveloper, request.IpAddress);
            RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredDto registeredDto = new()
            {
                AccessToken = createdAccessToken,
                RefreshToken = addedRefreshToken
            };
            return registeredDto;
        }
    }
}