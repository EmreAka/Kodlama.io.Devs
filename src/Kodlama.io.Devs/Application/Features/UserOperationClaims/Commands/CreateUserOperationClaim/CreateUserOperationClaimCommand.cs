using Application.Features.OperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;

public class CreateUserOperationClaimCommand : IRequest<CreatedOperationClaimDto>, ISecuredRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
    public string[] Roles { get; } = { "Admin" };

    class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand,
        CreatedOperationClaimDto>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            => (_userOperationClaimRepository, _userOperationClaimBusinessRules) =
                (userOperationClaimRepository, userOperationClaimBusinessRules);

        public async Task<CreatedOperationClaimDto> Handle(CreateUserOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            await _userOperationClaimBusinessRules.UserShouldExist(request.UserId);
            await _userOperationClaimBusinessRules.OperationClaimShouldExist(request.OperationClaimId);

            UserOperationClaim userOperationClaim = new()
            {
                UserId = request.UserId,
                OperationClaimId = request.OperationClaimId
            };

            var createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(userOperationClaim);

            var createdUserOperationClaimDto = new CreatedOperationClaimDto();

            return createdUserOperationClaimDto;
        }
    }
}