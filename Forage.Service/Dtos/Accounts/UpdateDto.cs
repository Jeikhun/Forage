using System.ComponentModel.DataAnnotations;

namespace Forage.Service.Dtos.Accounts
{
    public class UpdateDto
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; } = string.Empty;
    }
}
