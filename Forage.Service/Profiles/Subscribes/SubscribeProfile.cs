using AutoMapper;
using Forage.Core.Entities;
using Forage.Service.Dtos.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Profiles.Subscribes
{
    public class SubscribeProfile:Profile
    {
        public SubscribeProfile()
        {
            CreateMap<SubscribePostDto, Subscribe>();
        }
    }
}
