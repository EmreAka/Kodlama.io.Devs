using FluentValidation;

namespace Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;

public class CreateGitHubProfileCommandValidator : AbstractValidator<CreateGitHubProfileCommand>
{
    public CreateGitHubProfileCommandValidator()
    {
        RuleFor(g => g.DeveloperId).NotNull();
        RuleFor(g => g.ProfileUrl).NotEmpty().NotNull();
    }
}