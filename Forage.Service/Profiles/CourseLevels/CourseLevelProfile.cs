using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.CourseLevels;

namespace Forage.Service.Profiles.CourseCategories
{
    public class CourseLevelProfile : Profile
    {
        public CourseLevelProfile()
        {
            CreateMap<CourseLevelPostDto,CourseLevel>();
            CreateMap<CourseLevelUpdateDto, CourseLevel>();
            CreateMap<CourseLevel, CourseLevelUpdateDto>();
        }
    }
}
