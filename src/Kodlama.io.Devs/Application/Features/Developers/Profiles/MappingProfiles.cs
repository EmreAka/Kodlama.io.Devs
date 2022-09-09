using AutoMapper;
using Domain.Entities;
using Application.Features.Developers.Commands.CreateDeveloper;

namespace Application.Features.Developers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Developer, CreateDeveloperCommand>().ReverseMap();
    }
}