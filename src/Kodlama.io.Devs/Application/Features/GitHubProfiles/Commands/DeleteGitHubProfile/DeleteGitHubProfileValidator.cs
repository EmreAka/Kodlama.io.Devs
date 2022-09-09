using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;

public class DeleteGitHubProfileValidator : AbstractValidator<DeleteGitHubProfileCommand>
{
    public DeleteGitHubProfileValidator()
    {
        RuleFor(g => g.Id).NotNull();
    }
}