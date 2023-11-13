using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.Courses
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<CoursePostDto, Course>();
            CreateMap<CourseUpdateDto, Course>();
            CreateMap<Course, CourseUpdateDto>();
        }
    }
}
