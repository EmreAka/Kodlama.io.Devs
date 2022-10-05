using Application.Features.UserOperationClaims.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.UserOperationClaims.Models;

public class GetListUserOperationClaimModel: BasePageableModel
{
    public UserOperationClaimListDto[] Items { get; set; }
}