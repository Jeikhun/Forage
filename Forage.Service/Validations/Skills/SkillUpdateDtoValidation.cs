using FluentValidation;
using Forage.Service.Dtos.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Validations.Skills
{
    public class SkillUpdateDtoValidation : AbstractValidator<SkillUpdateDto>
    {
        public SkillUpdateDtoValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull().WithMessage("Name can not be null")
            .MinimumLength(3)
            .MaximumLength(50);
        }
    }
}
