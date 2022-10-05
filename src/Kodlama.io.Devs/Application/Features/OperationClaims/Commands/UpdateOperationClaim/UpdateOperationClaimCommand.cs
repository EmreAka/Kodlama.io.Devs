using Application.Features.OperationClaims.Rules;
using Application.Features.Technologies.Dtos;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.UpdateOperationClaim;

public class UpdateOperationClaimCommand : IRequest<UpdatedTechnologyDto>, ISecuredRequest
{
    public string[] Roles { get; } = { "Admin" };

    public OperationClaim OperationClaim { get; set; }

    public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedTechnologyDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository,
            OperationClaimBusinessRules operationClaimBusinessRules)
            => (_operationClaimRepository, _operationClaimBusinessRules) =
                (operationClaimRepository, operationClaimBusinessRules);

        public async Task<UpdatedTechnologyDto> Handle(UpdateOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            var operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == request.OperationClaim.Id);
            _operationClaimBusinessRules.OperationClaimShouldExistToUpdate(operationClaim);
            operationClaim.Name = request.OperationClaim.Name.ToLower();
            var updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);

            var updatedOperationClaimDto = new UpdatedTechnologyDto()
            {
                Id = updatedOperationClaim.Id,
                Name = updatedOperationClaim.Name
            };

            return updatedOperationClaimDto;
        }
    }
}