using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Domain.Entities;

namespace Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules
{
    private readonly IUserRepository _userRepository;
    private readonly IOperationClaimRepository _operationClaimRepository;

    public UserOperationClaimBusinessRules(IUserRepository userRepository,
        IOperationClaimRepository operationClaimRepository)
        => (_userRepository, _operationClaimRepository) = (userRepository, operationClaimRepository);

    public async Task UserShouldExist(int userId)
    {
        var result = await _userRepository.GetAsync(u => u.Id == userId);
        if (result is null)
            throw new BusinessException("This user does not exist");
    }

    public async Task OperationClaimShouldExist(int operationClaimId)
    {
        var result = await _operationClaimRepository.GetAsync(o => o.Id == operationClaimId);
        if (result is null)
            throw new BusinessException("This operation claim does not exist");
    }

    public void UserOperationClaimShouldExistToDelete(UserOperationClaim userOperationClaims)
    {
        if (userOperationClaims is null)
            throw new BusinessException("User operation claim does not exist");
    }
}