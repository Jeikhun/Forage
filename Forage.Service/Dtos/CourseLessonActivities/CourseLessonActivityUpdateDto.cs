using Forage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Dtos.CourseLessonActivities
{
    public record CourseLessonActivityUpdateDto
    {
        public bool IsFinished { get; set; }
        public float Percent { get; set; }
    }
}
