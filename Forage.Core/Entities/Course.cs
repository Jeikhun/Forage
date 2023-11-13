using Forage.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class Course:BaseEntitiy
    {
        public string Name { get; set; }
        public string CourseDuration { get; set; }
        public string About { get; set; }
        public string AboutImage { get; set; }
        public string AboutImageUrl { get; set; }
        public string AboutVideo { get; set; }
        public string AboutVideoUrl { get; set; }
        public string Language { get; set; }
        public string ShortInfo { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
        public int CourseCategoryId { get; set; }
        public int CourseLevelId { get; set; }
        public Company Company { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public List<CourseSkill>? CourseSkills { get; set; }
        public List<CourseLesson>? CourseLessons { get;set; }
    }
}
