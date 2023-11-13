using FluentValidation;
using Forage.Service.Dtos.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forage.Service.Validations.Skills
{
    public class SkillPostDtoValidation:AbstractValidator<SkillPostDto>
    {
        public SkillPostDtoValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull().WithMessage("Name can not be null")
            .MinimumLength(3)
            .MaximumLength(50);
        }
    }
}
