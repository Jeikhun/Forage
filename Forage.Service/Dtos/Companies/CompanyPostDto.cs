using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Dtos.Companies
{
    public class CompanyPostDto
    {
        public string Name { get; set; }
        public string Voen { get; set; }
        public IFormFile file { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CompanyIndustryFieldId { get; set; }
        public string AppUserId { get; set; }
    }
}
