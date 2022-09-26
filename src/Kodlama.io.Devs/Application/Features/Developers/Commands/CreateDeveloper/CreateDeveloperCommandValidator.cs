using FluentValidation;

namespace Application.Features.Developers.Commands.CreateDeveloper;

public class CreateUserCommandValidator : AbstractValidator<CreateDeveloperCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(d => d.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(d => d.Password).NotEmpty().NotNull().MinimumLength(9);
        RuleFor(d => d.FirstName).NotEmpty().NotNull();
        RuleFor(d => d.LastName).NotEmpty().NotNull();
    }
}