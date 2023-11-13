using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.Skills
{
    public class SkillProfile:Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillPostDto, Skill>();
            CreateMap<SkillUpdateDto, Skill>();
            CreateMap<Skill, SkillUpdateDto>();
        }
    }
}
