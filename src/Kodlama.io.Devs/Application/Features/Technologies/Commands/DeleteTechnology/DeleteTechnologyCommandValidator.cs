using FluentValidation;

namespace Application.Features.Technologies.Commands.DeleteTechnology;

public class DeleteTechnologyCommandValidator : AbstractValidator<DeleteTechnologyCommand>
{
    public DeleteTechnologyCommandValidator()
    {
        RuleFor(t => t.Id).NotNull();
    }
}