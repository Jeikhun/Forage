using Forage.Core.Entities.BaseEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class Company:BaseEntitiy
    {
        public string Name { get; set; }
        public string Voen { get; set; }
        public string Logo { get; set; }
        public string LogoUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CompanyIndustryFieldId { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public CompanyIndustryField? CompanyIndustryField { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
