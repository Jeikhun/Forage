using Forage.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Dtos.Courses
{
    public class CoursePostDto
    {
        public string Name { get; set; }
        public string CourseDuration { get; set; }
        public string About { get; set; }
        public IFormFile AboutImage { get; set; }
        public IFormFile AboutVideo { get; set; }
        public int CompanyId { get; set; }
        public int CourseCategoryId { get; set; }
        public int CourseLevelId { get; set; }
        public string Language { get; set; }
        public string ShortInfo { get; set; }
        public bool IsActive { get; set; }
        public List<int> SkillIds { get; set; }
    }
}
