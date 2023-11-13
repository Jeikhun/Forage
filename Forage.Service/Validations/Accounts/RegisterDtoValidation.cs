using FluentValidation;
using Forage.Service.Dtos.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forage.Service.Validations.Accounts
{
    public class RegisterDtoValidation:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(x => x.Username)
          .NotEmpty()
          .NotNull().WithMessage("User Name is required");
            RuleFor(x => x)
                .Custom((x, context) =>
                {
                    if (x.Email != null)
                    {
                        Regex re = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
                        if (!re.IsMatch(x.Email))
                        {
                            context.AddFailure("email", "Email format must be correct");
                        }
                    }
                    else
                    {
                        context.AddFailure("email", "Email is required");
                    }
                }
                );
            RuleFor(x => x.Password)
          .NotEmpty()
          .NotNull().WithMessage("Password is required");
        }
    }
}
