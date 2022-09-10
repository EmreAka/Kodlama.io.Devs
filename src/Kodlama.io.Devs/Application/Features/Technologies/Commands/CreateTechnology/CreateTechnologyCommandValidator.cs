using FluentValidation;

namespace Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(t => t.Name).NotEmpty().NotNull();
        RuleFor(t => t.ProgrammingLanguageId).NotNull();
    }
}