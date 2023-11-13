using Forage.Service.Dtos.Accounts;
using Forage.Service.Dtos.Common.ResponsesDtos;
using Forage.Service.Dtos.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface IIdentityUserService
    {
        public Task<List<AllUserGetDto>> GetAllUsers();
        public Task<BaseReponse> Register(RegisterDto registerDto, string role = null);

        public Task<TokenResponseDto> Login(LoginDto loginDto, int accessTokenLifeTime, string role = null);

        //public Task<BaseReponse> AddExternalUser(AddExternalUserDto dto);

        public Task<TokenResponseDto> GoogleAuthorize(string token);
        public Task<object> GetCurrentUser();

        public Task<object> UpdateUser(UpdateDto dto);
    }
}
