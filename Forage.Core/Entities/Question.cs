using Forage.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Core.Entities
{
    public class Question:BaseEntitiy
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
