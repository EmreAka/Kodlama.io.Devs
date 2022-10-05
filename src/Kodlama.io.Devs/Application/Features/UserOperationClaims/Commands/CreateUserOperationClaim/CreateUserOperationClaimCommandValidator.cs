using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;

public class CreateUserOperationClaimCommandValidator: AbstractValidator<CreateUserOperationClaimCommand>
{
    public CreateUserOperationClaimCommandValidator()
    {
        RuleFor(u => u.UserId).NotNull().GreaterThan(0);
        RuleFor(u => u.OperationClaimId).NotNull().GreaterThan(0);
    }
}