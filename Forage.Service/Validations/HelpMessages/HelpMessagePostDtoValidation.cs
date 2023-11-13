using FluentValidation;
using Forage.Service.Dtos.HelpMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forage.Service.Validations.HelpMessages
{
    public class HelpMessagePostDtoValidation:AbstractValidator<HelpMessagePostDto>
    {
        public HelpMessagePostDtoValidation()
        {
    
            RuleFor(x => x.FirstName)
             .NotEmpty()
            .NotNull().WithMessage("FirstName can not be null")
               .MinimumLength(3)
               .MaximumLength(15);
            RuleFor(x => x.LastName)
           .NotEmpty()
           .NotNull().WithMessage("LastName can not be null")
             .MinimumLength(3)
             .MaximumLength(15);
            RuleFor(x => x.Message)
  .NotEmpty()
  .NotNull().WithMessage("Message can not be null")
  .MinimumLength(3)
  .MaximumLength(400);
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
             }
             );
        }
    }
}
