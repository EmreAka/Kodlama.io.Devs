namespace Domain.Entities;

public class User : Core.Security.Entities.User
{
    public int GitHubProfileId { get; set; }

    public virtual ICollection<GitHubProfile> GitHubProfiles { get; set; }

    public User()
    {

    }


}
