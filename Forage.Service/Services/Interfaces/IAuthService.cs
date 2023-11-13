using Forage.Core.Entities;
using Forage.Service.Dtos.Auths;
using Forage.Service.Dtos.Common.ResponsesDtos;
using Forage.Service.Dtos.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<BaseReponse> UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto);
        public Task UpdateRefreshToken(AppUser user, string RefreshToken, DateTime accessTokenLifeTime, int addMinuteToLifeTime);
        public Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
        public Task<GeneralResponse<ResetTokenEmailResponseDto>> ResetPassword(string email);
        public Task<BaseReponse> VerifyPasswordResetToken(ResetResponseDto resetResponseDto);
    }
}
