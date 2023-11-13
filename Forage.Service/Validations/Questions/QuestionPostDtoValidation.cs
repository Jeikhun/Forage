using FluentValidation;
using Forage.Service.Dtos.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Forage.Service.Validations.Questions
{
    public class QuestionPostDtoValidation:AbstractValidator<QuestionPostDto>
    {
        public QuestionPostDtoValidation()
        {
            RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull().WithMessage("Title can not be null")
            .MinimumLength(3)
            .MaximumLength(40);
            RuleFor(x => x.Text)
                  .NotEmpty()
            .NotNull().WithMessage("Text can not be null")
            .MinimumLength(3)
            .MaximumLength(300);
        }
    }
}
