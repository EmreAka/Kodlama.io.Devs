using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Rules;

public class GithubProfileBusinessRules
{
    private readonly IGitHubProfileRepository _gitHubProfileRepository;

    public GithubProfileBusinessRules(IGitHubProfileRepository gitHubProfileRepository)
        => _gitHubProfileRepository = gitHubProfileRepository;

    public async Task GitHubProfileCanNotBeDuplicatedWhenInserted(int userId)
    {
        GitHubProfile result = await _gitHubProfileRepository.GetAsync(b => b.DeveloperId == userId);
        if (result != null) throw new BusinessException("There is already a GitHub profile assigned");
    }
    
    public void ProgrammingLanguageShouldExistWhenUpdated(GitHubProfile gitHubProfile)
    {
        if (gitHubProfile == null) throw new BusinessException("Requested GitHub profile does not exist");
    }
}
