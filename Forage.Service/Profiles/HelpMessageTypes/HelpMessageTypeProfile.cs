using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.HelpMessageType;
using Forage.Service.Dtos.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.HelpMessageTypes
{
    public class HelpMessageTypeProfile : Profile
    {
        public HelpMessageTypeProfile()
        {
            CreateMap<HelpMessageTypePostDto, HelpMessageType>();
            CreateMap<HelpMessageTypeUpdateDto, HelpMessageType>();
            CreateMap<HelpMessageType, HelpMessageTypeUpdateDto>();
        }
    }
}
