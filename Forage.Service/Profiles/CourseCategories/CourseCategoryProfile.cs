using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.CourseCategories;

namespace Forage.Service.Profiles.CourseCategories
{
    public class CourseCategoryProfile : Profile
    {
        public CourseCategoryProfile()
        {
            CreateMap<CourseCategoryPostDto,CourseCategory>();
            CreateMap<CourseCategoryUpdateDto, CourseCategory>();
            CreateMap<CourseCategory, CourseCategoryUpdateDto>();
        }
    }
}
