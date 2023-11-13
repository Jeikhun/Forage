using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.CourseLessons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.CourseLessons
{
    public class CourseLessonProfile:Profile
    {
        public CourseLessonProfile()
        {
            CreateMap<CourseLessonPostDto, CourseLesson>();
            CreateMap<CourseLessonUpdateDto, CourseLesson>();
            CreateMap<CourseLesson, CourseLessonUpdateDto>();
        }
    }
}
