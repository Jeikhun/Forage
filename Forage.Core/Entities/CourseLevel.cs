using Forage.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class CourseLevel:BaseEntitiy
    {
        public string Name { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
