using FluentValidation;
using Forage.Service.Dtos.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Validations.Accounts
{
    public class LoginDtoValidation:AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(x => x.Email)
          .NotEmpty()
          .NotNull().WithMessage("Email is required");
            RuleFor(x => x.Password)
          .NotEmpty()
          .NotNull().WithMessage("Password is required");
        }
    }
}
