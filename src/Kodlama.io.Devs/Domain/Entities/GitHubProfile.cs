using Core.Persistence.Repositories;

namespace Domain.Entities;

public class GitHubProfile : Entity
{
    public int UserId { get; set; }
    public string ProfileUrl { get; set; }
    public virtual User User { get; set; }

    public GitHubProfile()
    {

    }

    public GitHubProfile(int userId, string profileUrl) : this()
        => (UserId, ProfileUrl) = (userId, profileUrl);
}
