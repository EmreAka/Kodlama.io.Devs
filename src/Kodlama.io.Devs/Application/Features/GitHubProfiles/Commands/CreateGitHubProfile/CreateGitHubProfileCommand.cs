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
        private readonly IDeveloperRepository _developerRepository;
        public CreateGitHubProfileCommandHandler(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository, GithubProfileBusinessRules githubProfileBusinessRules, IDeveloperRepository developerRepository)
            => (_mapper, _gitHubProfileRepository, _githubProfileBusinessRules, _developerRepository) = (mapper, gitHubProfileRepository, githubProfileBusinessRules, developerRepository);

        public async Task<CreatedGitHubProfileDto> Handle(CreateGitHubProfileCommand request, CancellationToken cancellationToken)
        {
            GitHubProfile gitHubProfile = _mapper.Map<GitHubProfile>(request);

            await _githubProfileBusinessRules.GitHubProfileCanNotBeDuplicatedWhenInserted(request.DeveloperId);

            gitHubProfile = await _gitHubProfileRepository.AddAsync(gitHubProfile);

            Developer developer = await _developerRepository.GetAsync(d => d.Id == request.DeveloperId);
            developer.GitHubProfileId = gitHubProfile.Id;
            await _developerRepository.UpdateAsync(developer);

            CreatedGitHubProfileDto createdGitHubProfileDto = _mapper.Map<CreatedGitHubProfileDto>(gitHubProfile);

            return createdGitHubProfileDto;
        }
    }
}
