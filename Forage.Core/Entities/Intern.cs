using Forage.Core.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class Intern:BaseEntitiy
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthYear { get; set; }
        public string AppUserId { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public AppUser AppUser { get; set; }
    }
}
