using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;

public class CreateGitHubProfileCommand : IRequest<CreatedGitHubProfileDto>
{
    public int DeveloperId { get; set; }
    public string ProfileUrl { get; set; }

    public class CreateGitHubProfileCommandHandler : IRequestHandler<CreateGitHubProfileCommand, CreatedGitHubProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;
        private readonly GithubProfileBusinessRules _githubProfileBusinessRules;
        public CreateGitHubProfileCommandHandler(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository, GithubProfileBusinessRules githubProfileBusinessRules)
            => (_mapper, _gitHubProfileRepository, _githubProfileBusinessRules) = (mapper, gitHubProfileRepository, githubProfileBusinessRules);

        public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
        {
            GitHubProfile gitHubProfile = _mapper.Map<GitHubProfile>(request);

            await _githubProfileBusinessRules.GitHubProfileCanNotBeDuplicatedWhenInserted(request.DeveloperId);

            gitHubProfile = await _gitHubProfileRepository.AddAsync(gitHubProfile);

            CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(gitHubProfile);

            return createdGitHubProfileDto;
        }
    }
}
