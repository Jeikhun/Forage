using FluentValidation;
using Forage.Service.Dtos.Companies;
using Forage.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Validations.Companies
{
    public class CompanyUpdateDtoValidation : AbstractValidator<CompanyUpdateDto>
    {
        public CompanyUpdateDtoValidation()
        {
            RuleFor(x => x.Name)
                   .NotEmpty()
                   .NotNull().WithMessage("Name can not be null")
                   .MinimumLength(3)
                   .MaximumLength(40);
            RuleFor(x => x.Voen)
                  .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(30);
            RuleFor(x => x)
           .Custom((x, context) =>
           {
               if (x.file != null)
               {
                   if (!Helper.isImage(x.file))
                   {
                       context.AddFailure("file", "The type of file must be image");
                   }
                   if (!Helper.isSizeOk(x.file, 2))
                   {
                       context.AddFailure("file", "The size of image less than 2 mb");
                   }
               }
           });
            RuleFor(x => x.PhoneNumber)
                 .NotEmpty()
                 .NotNull().WithMessage("PhoneNumber can not be null");
            RuleFor(x => x.Address)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.AppUserId)
            .NotEmpty()
      .NotNull();
            RuleFor(x => x.CompanyIndustryFieldId)
                .NotEmpty()
                .NotNull();
        }
    }
}
