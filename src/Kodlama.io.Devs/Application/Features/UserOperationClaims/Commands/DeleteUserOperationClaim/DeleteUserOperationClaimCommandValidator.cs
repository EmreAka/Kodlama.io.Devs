using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;

public class DeleteUserOperationClaimCommandValidator: AbstractValidator<DeleteUserOperationClaimCommand>
{
    public DeleteUserOperationClaimCommandValidator()
    {
        RuleFor(u => u.Id).NotNull().GreaterThan(0);
    }
}