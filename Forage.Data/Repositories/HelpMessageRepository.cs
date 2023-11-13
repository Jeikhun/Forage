using Forage.Core.Entities;
using Forage.Core.Repositories;
using Forage.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Data.Repositories
{
    public class HelpMessageRepository : Repository<HelpMessage>, IHelpMessageRepository
    {
        public HelpMessageRepository(ForageAppDbContext context) : base(context)
        {

        }
    }
}
