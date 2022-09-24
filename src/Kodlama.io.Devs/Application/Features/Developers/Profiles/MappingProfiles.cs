using AutoMapper;
using Domain.Entities;
using Core.Security.JWT;
using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Dtos;
using Application.Features.Developers.Models;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.Developers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Developer, CreateDeveloperCommand>().ReverseMap();
        CreateMap<TokenDto, AccessToken>().ReverseMap();
        
        CreateMap<User, UserListDto>()
            .ForMember(c => c.OperationClaims,
                opt => opt.MapFrom(c => c.UserOperationClaims.Select(x => x.OperationClaim))).ReverseMap();
        CreateMap<IPaginate<User>, UserListModel>().ReverseMap();
    }
}