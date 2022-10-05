using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.Entities;

namespace Application.Features.OperationClaims.Rules;

public class OperationClaimBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        => (_operationClaimRepository) = (operationClaimRepository);

    public async Task OperationClaimShouldNotDuplicatedWhenInserted(string role)
    {
        var result = await _operationClaimRepository.GetAsync(o => 
            o.Name.ToLower() == role.ToLower());

        if (result is not null)
            throw new BusinessException("This role already exists");
    }

    public void OperationClaimShouldExistToDelete(OperationClaim operationClaim)
    {
        if (operationClaim is null)
            throw new BusinessException("This role does not exist");
    }
}