using AutoMapper;
using Domain.Entities;
using Core.Security.JWT;
using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Dtos;

namespace Application.Features.Developers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Developer, CreateDeveloperCommand>().ReverseMap();
        CreateMap<TokenDto, AccessToken>().ReverseMap();
    }
}