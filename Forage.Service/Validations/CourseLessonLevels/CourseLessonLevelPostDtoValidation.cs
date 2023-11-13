using FluentValidation;
using Forage.Service.Dtos.CourseLessonLevels;
using Forage.Service.Dtos.HelpMessageType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Validations.CourseCategories
{
    public class CourseLessonLevelPostDtoValidation: AbstractValidator<CourseLessonLevelPostDto>
    {
        public CourseLessonLevelPostDtoValidation()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull().WithMessage("Name can not be null")
            .MinimumLength(3)
            .MaximumLength(30);
        }
    }
}
