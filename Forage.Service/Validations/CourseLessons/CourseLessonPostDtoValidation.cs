using FluentValidation;
using Forage.Service.Dtos.CourseLessons;
using Forage.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forage.Service.Validations.CourseLessons
{
    public class CourseLessonPostDtoValidation:AbstractValidator<CourseLessonPostDto>
    {
        public CourseLessonPostDtoValidation()
        {
            RuleFor(x => x.Name)
                  .NotEmpty()
                  .NotNull().WithMessage("Name can not be null")
                  .MinimumLength(3)
                  .MaximumLength(50);
            RuleFor(x => x.CourseDuration)
                  .NotEmpty()
                  .NotNull()
                  .MinimumLength(3)
                  .MaximumLength(50);
            RuleFor(x => x.Description)
                  .NotEmpty()
                  .NotNull()
                  .MinimumLength(200)
                  .MaximumLength(500);
            RuleFor(x => x)
                  .Custom((x, context) =>
               {
                   if (x.Video != null)
                   {
                    if (!Helper.isVideo(x.Video))
                    {
                        context.AddFailure("file", "The type of file must be video");
                    }
                    if (!Helper.isSizeOk(x.Video, 20))
                    {
                        context.AddFailure("file", "The size of video less than 20 mb");
                    }
               }
              });
            RuleFor(x => x.LessonLine)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.CourseId)
              .NotEmpty()
              .NotNull();
            RuleFor(x => x.CourseLessonLevelId)
                .NotEmpty()
                .NotNull();
          
        }
    }
}
