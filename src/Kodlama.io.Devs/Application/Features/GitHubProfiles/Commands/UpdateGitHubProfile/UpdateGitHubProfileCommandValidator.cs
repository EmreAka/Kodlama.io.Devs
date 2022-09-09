using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;

public class UpdateGitHubProfileCommandValidator : AbstractValidator<UpdateGitHubProfileCommand>
{
    public UpdateGitHubProfileCommandValidator()
    {
        RuleFor(g => g.Id).NotNull();
        RuleFor(g => g.ProfileUrl).NotNull().NotEmpty();
    }
}