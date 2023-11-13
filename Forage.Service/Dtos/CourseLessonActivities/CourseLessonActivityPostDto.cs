using Forage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Dtos.CourseLessonActivities
{
    public record CourseLessonActivityPostDto
    {
        public int CourseLessonId { get; set; }
        public float Percent { get; set; }
        public bool isFinished { get; set; }
    }
}
