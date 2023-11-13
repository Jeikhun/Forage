using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Dtos.Interns
{
    public class InternPostDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BirthYear { get; set; }
        public string AppUserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
