using Application.Features.GitHubProfiles.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Attributes;
using Domain.Entities;
using MediatR;

namespace Application.Features.GitHubProfiles.Queries.GetListGitHubProfile;

[Authorize(Roles = new[] { "admin" })]
public class GetListGitHubProfileQuery : PageRequest ,IRequest<GithubProfileListModel>, ISecuredRequest
{
    public class GetListGitHubProfileQueryHandler : IRequestHandler<GetListGitHubProfileQuery, GithubProfileListModel>
    {
        private readonly IMapper _mapper;
        private readonly IGitHubProfileRepository _gitHubProfileRepository;

        public GetListGitHubProfileQueryHandler(IMapper mapper, IGitHubProfileRepository gitHubProfileRepository)
            => (_mapper, _gitHubProfileRepository) = (mapper, gitHubProfileRepository);

        public async Task<GithubProfileListModel> Handle(GetListGitHubProfileQuery request,
            CancellationToken cancellationToken)
        {
            IPaginate<GitHubProfile> profiles = await _gitHubProfileRepository
                .GetListAsync(index: request.Page, size: request.PageSize);

            GithubProfileListModel githubProfileListModel = _mapper.Map<GithubProfileListModel>(profiles);

            return githubProfileListModel;
        }
    }
}