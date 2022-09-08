namespace Application.Features.Users.Dtos;

public class TokenDto
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
