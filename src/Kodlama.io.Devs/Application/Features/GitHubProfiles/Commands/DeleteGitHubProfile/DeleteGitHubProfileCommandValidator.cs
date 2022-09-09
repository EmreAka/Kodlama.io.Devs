using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;

public class DeleteGitHubProfileCommandValidator : AbstractValidator<DeleteGitHubProfileCommand>
{
    public DeleteGitHubProfileCommandValidator()
    {
        RuleFor(g => g.Id).NotNull();
    }
}