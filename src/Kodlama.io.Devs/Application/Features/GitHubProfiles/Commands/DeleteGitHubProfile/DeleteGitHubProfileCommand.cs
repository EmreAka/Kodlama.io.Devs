using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;

public class DeleteGitHubProfileCommand : IRequest<DeletedGitHubProfileDto>
{
    public int Id { get; set; }

    public class DeleteGitHubProfileCommandQuery : IRequestHandler<DeleteGitHubProfileCommand, DeletedGitHubProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;
        private readonly GithubProfileBusinessRules _githubProfileBusinessRules;

        public DeleteGitHubProfileCommandQuery(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository,
            GithubProfileBusinessRules githubProfileBusinessRules)
            => (_mapper, _gitHubProfileRepository, _githubProfileBusinessRules) =
                (mapper, gitHubProfileRepository, githubProfileBusinessRules);

        public async Task<DeletedGitHubProfileDto> Handle(DeleteGitHubProfileCommand request,
            CancellationToken cancellationToken)
        {
            GitHubProfile gitHubProfile = await _gitHubProfileRepository.GetAsync(g => g.Id == request.Id);
            _githubProfileBusinessRules.ProgrammingLanguageShouldExistWhenDeleted(gitHubProfile);

            gitHubProfile = await _gitHubProfileRepository.DeleteAsync(gitHubProfile);

            DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(gitHubProfile);

            return deletedGitHubProfileDto;
        }
    }
}