using Core.Security.Entities;
using Core.Security.Hashing;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Features.Developers.Rules;

public class DeveloperBusinessRules
{
    public void UserShouldExist(User user)
    {
        if (user == null) throw new BusinessException("User does not exist");
    }

    public void UserCredentialsShouldMatch(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        if (!result) throw new BusinessException("Wrong credentials");
    }
}