using FluentValidation;
using Forage.Service.Dtos.Auths;

namespace Forage.Service.Validations.Auths
{
    public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordDtoValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty().NotNull().WithMessage("Enter UserId");
            RuleFor(u => u.ResetToken)
                .NotEmpty().NotNull().WithMessage("Enter Reset Token");
            RuleFor(u => u.NewPassword)
                .NotEmpty().NotNull().WithMessage("Enter New Password");
            RuleFor(u => u.ConfirmedNewPassword)
                .Equal(u=>u.NewPassword).WithMessage("Password and Confirmed Password must be same")
                .NotEmpty().NotNull().WithMessage("Enter Confirmed Password");
        }
    }
}
