using Application.Features.GitHubProfiles.Commands.CreateGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.GitHubProfiles.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
		CreateMap<GitHubProfile, CreateGitHubProfileCommand>().ReverseMap();
        CreateMap<GitHubProfile, CreatedGitHubProfileDto>().ReverseMap();
        
        CreateMap<GitHubProfile, UpdateGitHubProfileCommand>().ReverseMap();
        CreateMap<GitHubProfile, UpdatedGitHubProfileDto>().ReverseMap();

        CreateMap<GitHubProfile, DeleteGitHubProfileCommand>().ReverseMap();
        CreateMap<GitHubProfile, DeletedGitHubProfileDto>().ReverseMap();

        //getlistquery
        CreateMap<GithubProfileListModel, IPaginate<GitHubProfile>>().ReverseMap();
        CreateMap<GitHubProfile, GithubProfileListDto>().ReverseMap();
    }
}
