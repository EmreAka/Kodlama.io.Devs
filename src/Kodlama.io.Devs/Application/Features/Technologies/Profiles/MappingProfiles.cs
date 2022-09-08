using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Technologies.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Technology, TechnologyListDto>()
            .ForMember(c => c.ProgrammingLanguageName,
                opt =>
                    opt.MapFrom(c => c.ProgrammingLanguage!.Name)).ReverseMap();
        
        CreateMap<IPaginate<Technology>, TechnologyListModel>().ReverseMap();
    }
}