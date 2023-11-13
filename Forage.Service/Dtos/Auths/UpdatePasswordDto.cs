namespace Forage.Service.Dtos.Auths
{
    public class UpdatePasswordDto
    {
        public string UserId { get; set; }
        public string ResetToken { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmedNewPassword { get; set; }
    }
}
