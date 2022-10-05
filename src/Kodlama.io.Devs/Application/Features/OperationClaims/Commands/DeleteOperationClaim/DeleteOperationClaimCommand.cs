using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimDto>, ISecuredRequest
{
    public int Id { get; set; }
    public string[] Roles { get; } = { "Admin" };

    public class
        DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimDto>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository,
            OperationClaimBusinessRules operationClaimBusinessRules)
            => (_operationClaimRepository, _operationClaimBusinessRules) =
                (operationClaimRepository, operationClaimBusinessRules);

        public async Task<DeletedOperationClaimDto> Handle(DeleteOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            var operationClaim = await _operationClaimRepository.GetAsync(o => o.Id == request.Id);
            _operationClaimBusinessRules.OperationClaimShouldExistToDelete(operationClaim);

            var result = await _operationClaimRepository.DeleteAsync(operationClaim);

            var deletedOperationClaimDto = new DeletedOperationClaimDto();

            return deletedOperationClaimDto;
        }
    }
}