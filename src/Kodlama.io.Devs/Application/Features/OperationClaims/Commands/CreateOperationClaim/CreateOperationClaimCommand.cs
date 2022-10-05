using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaim>, ISecuredRequest
{
    public string Role { get; set; }
    public string[] Roles { get; } = { "Admin" };


    public class
        CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaim>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository,
            OperationClaimBusinessRules operationClaimBusinessRules)
            => (_operationClaimRepository, _operationClaimBusinessRules) =
                (operationClaimRepository, operationClaimBusinessRules);

        public async Task<CreatedOperationClaim> Handle(CreateOperationClaimCommand request,
            CancellationToken cancellationToken)
        {
            await _operationClaimBusinessRules.OperationClaimShouldNotDuplicatedWhenInserted(request.Role);

            OperationClaim operationClaim = new()
            {
                Name = request.Role.ToLower()
            };

            var result = await _operationClaimRepository.AddAsync(operationClaim);

            CreatedOperationClaim createdOperationClaim = new();

            return createdOperationClaim;
        }
    }
}