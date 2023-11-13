using Forage.Core.Entities;
using Forage.Service.Dtos.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface ITokenHandler
    {
        public Task<TokenResponseDto> CreateAccessTokenAsync(AppUser user, int minute);
        public string CreateRefreshToken();
    }
}
