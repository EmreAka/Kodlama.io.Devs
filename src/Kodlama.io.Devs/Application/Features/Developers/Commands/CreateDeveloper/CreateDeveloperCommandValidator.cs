using FluentValidation;

namespace Application.Features.Developers.Commands.CreateDeveloper;

public class CreateUserCommandValidator : AbstractValidator<CreateDeveloperCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(d => d.UserForRegisterDto.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(d => d.UserForRegisterDto.Password).NotEmpty().NotNull().MinimumLength(9);
        RuleFor(d => d.UserForRegisterDto.FirstName).NotEmpty().NotNull();
        RuleFor(d => d.UserForRegisterDto.LastName).NotEmpty().NotNull();
    }
}