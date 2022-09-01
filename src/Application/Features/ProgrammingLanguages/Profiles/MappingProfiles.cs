using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Profiles;

public class MappingProfiles: Profile
{
	public MappingProfiles()
	{
		//ProgrammingLanguage Query.
		CreateMap<ProgrammingLanguageListModel, IPaginate<ProgrammingLanguage>>().ReverseMap();
		CreateMap<ProgrammingLanguage, ProgrammingLanguageListDto>().ReverseMap();

		CreateMap<ProgrammingLanguageGetByIdDto, ProgrammingLanguage>().ReverseMap();

		//Programming Language Command.
		CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
		CreateMap<CreatedProgrammingLanguageDto, ProgrammingLanguage>().ReverseMap();
	}
}
