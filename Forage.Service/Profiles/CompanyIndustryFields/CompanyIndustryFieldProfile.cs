using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.CompanyIndustryFields;
using Forage.Service.Dtos.CourseCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.CompanyIndustryFields
{
    public class CompanyIndustryFieldProfile : Profile
    {
        public CompanyIndustryFieldProfile()
        {
            CreateMap<CompanyIndustryFieldPostDto, CompanyIndustryField>(); 
            CreateMap<CompanyIndustryFieldUpdateDto, CompanyIndustryField>();
            CreateMap<CompanyIndustryField, CompanyIndustryFieldUpdateDto>();
        }
    }
}
