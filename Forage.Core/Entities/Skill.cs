using Forage.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class Skill:BaseEntitiy
    {
        public string Name { get; set; }
        public List<CourseSkill>? CourseSkills { get; set; }
    }
}
