using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;

public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string[] Roles { get; } = { "Admin" };

    class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand,
        DeletedUserOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            => (_userOperationClaimRepository, _userOperationClaimBusinessRules) =
                (userOperationClaimRepository, userOperationClaimBusinessRules);

        public async Task<DeletedUserOperationClaimDto> Handle(DeleteUserOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            var userOperationClaim = await _userOperationClaimRepository.GetAsync(u => u.Id == request.Id);

            _userOperationClaimBusinessRules.UserOperationClaimShouldExistToDelete(userOperationClaim);

            var deletedUserOperationClaim = await _userOperationClaimRepository.DeleteAsync(userOperationClaim);

            var deletedUserOperationClaimDto = new DeletedUserOperationClaimDto();

            return deletedUserOperationClaimDto;
        }
    }
}