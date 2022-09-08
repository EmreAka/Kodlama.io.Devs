using Core.Security.Entities;
using Core.Security.Enums;

namespace Domain.Entities;

public class Developer : User
{
    public int GitHubProfileId { get; set; }

    public virtual ICollection<GitHubProfile> GitHubProfiles { get; set; }

    public Developer()
    {

    }

    public Developer(int id, string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, bool status, AuthenticatorType authenticatorType, int gitHubProfileId) : base(id, firstName, lastName, email, passwordSalt, passwordHash, status, authenticatorType)
        => GitHubProfileId = gitHubProfileId;
}
