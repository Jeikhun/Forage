using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.Interns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.Interns
{
    public class InternProfile:Profile
    {
        public InternProfile()
        {
            CreateMap<InternPostDto, Intern>();
            CreateMap<InternUpdateDto, Intern>();
            CreateMap<Intern, InternUpdateDto>();
        }
    }
}
