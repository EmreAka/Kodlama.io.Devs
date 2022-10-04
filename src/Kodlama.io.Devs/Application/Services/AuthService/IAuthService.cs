using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.AuthService;

public interface IAuthService
{
    Task<AccessToken> CreateAccessToken(User user);
    Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
    Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
}