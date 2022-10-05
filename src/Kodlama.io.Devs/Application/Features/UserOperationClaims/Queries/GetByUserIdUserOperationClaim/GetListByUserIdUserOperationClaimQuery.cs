using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Models;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserOperationClaims.Queries.GetByUserIdUserOperationClaim;

public class GetListByUserIdUserOperationClaimQuery : IRequest<GetListUserOperationClaimModel>, ISecuredRequest
{
    public int UserId { get; set; }
    public PageRequest PageRequest { get; set; }
    public string[] Roles { get; } = { "Admin" };

    class GetByUserIdUserOperationClaimQueryHandler : IRequestHandler<GetListByUserIdUserOperationClaimQuery,
        GetListUserOperationClaimModel>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public GetByUserIdUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository)
            => _userOperationClaimRepository = userOperationClaimRepository;

        public async Task<GetListUserOperationClaimModel> Handle(GetListByUserIdUserOperationClaimQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _userOperationClaimRepository.GetListAsync(
                u => u.User.Id == request.UserId,
                include: m => m.Include(u => u.OperationClaim)
                    .Include(u => u.User),
                index:
                request.PageRequest.Page,
                size:
                request.PageRequest.PageSize);

            GetListUserOperationClaimModel getListUserOperationClaimModel = new()
            {
                Count = result.Count,
                Index = result.Index,
                Pages = result.Pages,
                Size = result.Size,
                HasNext = result.HasNext,
                HasPrevious = result.HasPrevious,
                Items = result.Items.Select(x => new UserOperationClaimListDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    OperationClaimId = x.OperationClaimId,
                    Name = x.OperationClaim.Name,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName
                }).ToArray()
            };

            return getListUserOperationClaimModel;
        }
    }
}