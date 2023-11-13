namespace Forage.Service.Dtos.Tokens
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
