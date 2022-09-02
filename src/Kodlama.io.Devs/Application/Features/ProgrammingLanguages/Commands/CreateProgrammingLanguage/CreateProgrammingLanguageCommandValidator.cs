using FluentValidation;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommandValidator : AbstractValidator<CreateProgrammingLanguageCommand>
{
	public CreateProgrammingLanguageCommandValidator()
	{
		RuleFor(p => p.Name).NotEmpty().NotNull();
	}
}
