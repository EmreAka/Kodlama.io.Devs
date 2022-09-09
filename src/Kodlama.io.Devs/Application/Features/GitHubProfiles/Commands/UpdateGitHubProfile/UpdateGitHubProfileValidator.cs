using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;

public class UpdateGitHubProfileValidator : AbstractValidator<UpdateGitHubProfileCommand>
{
    public UpdateGitHubProfileValidator()
    {
        RuleFor(g => g.Id).NotNull();
        RuleFor(g => g.ProfileUrl).NotNull().NotEmpty();
    }
}