using FluentValidation;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim;

public class UpdateOperationClaimCommandValidator: AbstractValidator<UpdateOperationClaimCommand>
{
    public UpdateOperationClaimCommandValidator()
    {
        RuleFor(o => o.OperationClaim.Name).NotNull().NotEmpty();
        RuleFor(o => o.OperationClaim.Id).NotNull().GreaterThan(0);
    }
}