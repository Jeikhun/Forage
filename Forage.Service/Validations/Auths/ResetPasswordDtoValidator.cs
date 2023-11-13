using FluentValidation;
using Forage.Service.Dtos.Auths;

namespace Forage.Service.Validations.Auths
{
    public class ResetPasswordDtoValidator :AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(r => r.Email).EmailAddress().NotEmpty().NotNull().WithMessage("Enter Email");
        }
    }
}
