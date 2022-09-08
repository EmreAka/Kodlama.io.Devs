using Application.Features.GitHubProfiles.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.GitHubProfiles.Models;

public class GithubProfileListModel : BasePageableModel
{
    public IList<GithubProfileListDto> Items { get; set; }
}
