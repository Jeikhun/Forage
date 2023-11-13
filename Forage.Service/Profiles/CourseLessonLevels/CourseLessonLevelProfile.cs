using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.CourseLessonLevels;

namespace Forage.Service.Profiles.CourseLessonLevels { 
    public class CourseLessonLevelProfile : Profile
    {
        public CourseLessonLevelProfile()
        {
            CreateMap<CourseLessonLevelPostDto,CourseLessonLevel>();
            CreateMap<CourseLessonLevelUpdateDto, CourseLessonLevel>();
            CreateMap<CourseLessonLevel, CourseLessonLevelUpdateDto>();
        }
    }
}
