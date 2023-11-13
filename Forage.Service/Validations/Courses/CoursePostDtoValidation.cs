using FluentValidation;
using Forage.Service.Dtos.Courses;
using Forage.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forage.Service.Validations.Courses
{
    public class CoursePostDtoValidation:AbstractValidator<CoursePostDto>
    {
        public CoursePostDtoValidation()
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
            RuleFor(x => x.About)
                  .NotEmpty()
                  .NotNull()
                  .MinimumLength(200)
                  .MaximumLength(500);
            RuleFor(x => x.ShortInfo)
             .NotEmpty()
             .NotNull()
             .MinimumLength(50)
             .MaximumLength(100);
            RuleFor(x => x.Language)
             .NotEmpty()
             .NotNull();
            RuleFor(x => x)
                  .Custom((x, context) =>
               {
                   if (x.AboutImage != null)
                   {
                    if (!Helper.isImage(x.AboutImage))
                    {
                        context.AddFailure("file", "The type of file must be image");
                    }
                    if (!Helper.isSizeOk(x.AboutImage, 2))
                    {
                        context.AddFailure("file", "The size of image less than 2 mb");
                    }
               }
               });

            RuleFor(x => x)
                  .Custom((x, context) =>
               {
                   if (x.AboutVideo != null)
                   {
                    if (!Helper.isVideo(x.AboutVideo))
                    {
                        context.AddFailure("file", "The type of file must be video");
                    }
                    if (!Helper.isSizeOk(x.AboutVideo, 20))
                    {
                        context.AddFailure("file", "The size of video less than 20 mb");
                    }
               }
              });
            RuleFor(x => x.CourseLevelId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.CourseCategoryId)
              .NotEmpty()
              .NotNull();
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .NotNull();
          
        }
    }
}
