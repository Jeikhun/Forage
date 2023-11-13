namespace Forage.Service.Dtos.Auths
{
    public class ResetResponseDto
    {
        public string UserId { get; set; } = null!;
        public string ResetToken { get; set; } = null!;
    }
}
