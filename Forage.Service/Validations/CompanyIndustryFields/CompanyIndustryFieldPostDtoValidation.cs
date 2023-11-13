using FluentValidation;
using Forage.Service.Dtos.CompanyIndustryFields;
using Forage.Service.Dtos.CourseLevels;
using Forage.Service.Dtos.HelpMessageType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Validations.CompanyIndustryFields
{
    public class CompanyIndustryFieldPostDtoValidation: AbstractValidator<CompanyIndustryFieldPostDto>
    {
        public CompanyIndustryFieldPostDtoValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull().WithMessage("Name can not be null")
            .MinimumLength(3)
            .MaximumLength(30);
        }
    }
}
