using FluentValidation;
using Forage.Service.Dtos.CourseLessonActivities;
using Forage.Service.Dtos.HelpMessageType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Validations.CourseLessonActivities
{
    public class CourseLessonActivityPostDtoValidation: AbstractValidator<CourseLessonActivityPostDto>
    {
        public CourseLessonActivityPostDtoValidation()
        {
            RuleFor(x => x.CourseLessonId)
            .NotEmpty()
            .NotNull();
            RuleFor(x => x.Percent)
           .NotEmpty();
        }
    }
}
