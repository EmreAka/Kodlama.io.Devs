using Core.Security.Hashing;
using Core.CrossCuttingConcerns.Exceptions;
using Application.Services.Repositories;
using Core.Domain.Entities;

namespace Application.Features.Developers.Rules;

public class DeveloperBusinessRules
{
    private readonly IUserRepository _userRepository;
    public DeveloperBusinessRules(IUserRepository userRepository)
        => _userRepository = userRepository;

    public void UserShouldExist(User user)
    {
        if (user == null) throw new BusinessException("User does not exist");
    }

    public void UserCredentialsShouldMatch(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
        if (!result) throw new BusinessException("Wrong credentials");
    }

    public async Task EmailCanNotBeDuplicatedWhenInserted(string email)
    {
        var result = await _userRepository.GetAsync(u => u.Email.ToLower().Equals(email.ToLower()));
        if (result != null) throw new BusinessException("This email is already registered");
    }
}