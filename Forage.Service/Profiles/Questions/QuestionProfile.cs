using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.Questions
{
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionPostDto, Question>();
            CreateMap<QuestionUpdateDto, Question>();
            CreateMap<Question, QuestionUpdateDto>();
        }
    }
}
