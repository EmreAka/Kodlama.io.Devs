using FluentValidation;

namespace Application.Features.Developers.Commands.LoginDeveloper;

public class LoginUserCommandValidator : AbstractValidator<LoginDeveloperCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(d => d.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(d => d.Password).NotEmpty().NotNull().MinimumLength(9);
    }
}