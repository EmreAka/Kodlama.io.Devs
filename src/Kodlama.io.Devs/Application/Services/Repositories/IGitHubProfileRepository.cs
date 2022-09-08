using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IGitHubProfileRepository : IAsyncRepository<GitHubProfile>, IRepository<GitHubProfile>
{
}
