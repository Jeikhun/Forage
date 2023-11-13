using Forage.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class CourseSkill:BaseEntitiy
    {
        public Course Course { get; set; }
        public Skill Skill { get; set; }
        public int CourseId { get; set; }
        public int SkillId { get; set; }
    }
}
