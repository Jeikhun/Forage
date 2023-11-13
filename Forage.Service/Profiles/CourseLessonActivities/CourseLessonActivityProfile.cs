using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.CourseLessonActivities;

namespace Forage.Service.Profiles.CourseLessonActivitys { 
    public class CourseLessonActivityProfile : Profile
    {
        public CourseLessonActivityProfile()
        {
            CreateMap<CourseLessonActivityPostDto,CourseLessonActivity>();
            CreateMap<CourseLessonActivityUpdateDto, CourseLessonActivity>();
            CreateMap<CourseLessonActivity, CourseLessonActivityUpdateDto>();
        }
    }
}
