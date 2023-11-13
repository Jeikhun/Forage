using Forage.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class CourseLessonActivity:BaseEntitiy
    {
        public string AppUserId { get; set; }
        public int CourseLessonId { get; set; }
        public CourseLesson CourseLesson { get; set; }
        public bool IsFinished { get; set; }
        public float Percent { get; set; }
    }
}
