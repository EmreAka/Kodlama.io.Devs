using FluentValidation;

namespace Application.Features.Technologies.Commands.UpdateTechnology;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
{
    public UpdateTechnologyCommandValidator()
    {
        RuleFor(t => t.Id).NotNull();
        RuleFor(t => t.Name).NotNull().NotEmpty();
        RuleFor(t => t.ProgrammingLanguageId).NotNull();
    }
}