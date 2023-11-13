﻿using FluentValidation;
using Forage.Service.Dtos.HelpMessageType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Validations.HelpMessageTypes
{
    public class HelpMessageTypeUpdateDtoValidation:AbstractValidator<HelpMessageTypeUpdateDto>
    {
        public HelpMessageTypeUpdateDtoValidation()
        {
            RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull().WithMessage("Title can not be null")
            .MinimumLength(3)
            .MaximumLength(70);
        }
    }
}
