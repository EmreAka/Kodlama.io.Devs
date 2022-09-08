using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;

public class UpdateGitHubProfileCommand : IRequest<UpdatedGitHubProfileDto>
{
    public int Id { get; set; }
    public string ProfileUrl { get; set; }

    public class
        UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand, UpdatedGitHubProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;
        private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

        public UpdateGitHubProfileCommandHandler(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository,
            GithubProfileBusinessRules githubProfileBusinessRules)
            => (_mapper, _gitHubProfileRepository, _githubProfileBusinessRules) =
                (mapper, gitHubProfileRepository, githubProfileBusinessRules);

        public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request,
            CancellationToken cancellationToken)
        {
            GitHubProfile gitHubProfile = await _gitHubProfileRepository.GetAsync(g => g.Id == request.Id);
            
            _githubProfileBusinessRules.ProgrammingLanguageShouldExistWhenUpdated(gitHubProfile);

            gitHubProfile = await _gitHubProfileRepository.UpdateAsync(_mapper.Map(request, gitHubProfile));

            UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(gitHubProfile);

            return updatedGitHubProfileDto;
        }
    }
}