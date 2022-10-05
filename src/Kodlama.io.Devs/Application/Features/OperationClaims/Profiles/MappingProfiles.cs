using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using AutoMapper;
using Core.Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.OperationClaims.Profiles;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim, OperationClaimListDto>().ReverseMap();
        CreateMap<OperationClaimListModel, OperationClaimListDto>().ReverseMap();
        CreateMap<IPaginate<OperationClaim>, OperationClaimListModel>().ReverseMap();
    }
}