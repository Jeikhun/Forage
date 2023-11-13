using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.HelpMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.HelpMessages
{
    public class HelpMessageProfile : Profile
    {
        public HelpMessageProfile()
        {
            CreateMap<HelpMessagePostDto, HelpMessage>();
        }
    }
}
