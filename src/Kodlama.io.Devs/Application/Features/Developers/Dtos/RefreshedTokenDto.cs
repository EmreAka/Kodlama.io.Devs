using Core.Domain.Entities;
using Core.Security.JWT;

namespace Application.Features.Developers.Dtos;

public class RefreshedTokenDto
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}