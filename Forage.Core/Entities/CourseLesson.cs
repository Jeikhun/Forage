using Forage.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class CourseLesson:BaseEntitiy
    {
        public string Name { get; set; }
        public string CourseDuration { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string VideoUrl { get; set; }
        public int LessonLine { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int CourseLessonLevelId { get; set; }
        public CourseLessonLevel CourseLessonLevel { get; set; }
    }
}
