using Forage.Core.Entities;
using Forage.Service.Dtos.Auths;
using Forage.Service.Dtos.Common.ResponsesDtos;
using Forage.Service.Dtos.Tokens;
using Forage.Service.Exceptions.AuthExceptions;
using Forage.Service.Extensions;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Forage.Service.Services.Implementations
{
    public class AuthService:IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly IMailService _mailService;

        public AuthService(UserManager<AppUser> userManager, ITokenHandler tokenHandler, IMailService mailService)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _mailService = mailService;
        }

        public async Task<GeneralResponse<ResetTokenEmailResponseDto>> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            ResetTokenEmailResponseDto emailResponseDto = new();
            if (user is not null)
            {
                var resetTokenResponse = await _userManager.GeneratePasswordResetTokenAsync(user);
                var newResetToken = resetTokenResponse.EncodeToken();
                emailResponseDto.Email = email;
                emailResponseDto.Token = newResetToken;
                emailResponseDto.UserId = user.Id;
                string resetLink = $"http://localhost:7297/resetPassword?token={newResetToken}&userId={user.Id}";

                await _mailService.SendEmailAsync(new Forage.Service.Dtos.Mails.MailRequestDto
                {
                    Attachments = null,
                    Subject = "ResetPassword",
                    Body = $"Please click on the link below to reset your password: \n {resetLink}",
                    ToEmails = new List<string> { user.Email }
                }); ;
                return new()
                {
                    Data = emailResponseDto,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = " Reset Token Generated"
                };
            }
            return new()
            {
                Data = null,
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = " Reset not generated"
            };
        }
        public async Task<BaseReponse> VerifyPasswordResetToken(ResetResponseDto resetResponseDto)
        {
            var user = await _userManager.FindByIdAsync(resetResponseDto.UserId);
            bool isValidToken = false;
            if (user is not null)
            {
                var decodedResetToken = resetResponseDto.ResetToken.DecodeToken();
                isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", decodedResetToken);
            }
            return new()
            {
                StatusCode = (int)(isValidToken ? HttpStatusCode.OK : HttpStatusCode.BadRequest),
                Message = $"{isValidToken}"
            };
        }

        public async Task<BaseReponse> UpdatePasswordAsync(UpdatePasswordDto updatePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(updatePasswordDto.UserId);
            bool isUpdated = false;
            if (user is not null)
            {
                var decodedResetToken = updatePasswordDto.ResetToken.DecodeToken();
                IdentityResult resetPassword = await _userManager.ResetPasswordAsync(user, decodedResetToken, updatePasswordDto.NewPassword);
                if (resetPassword.Succeeded)
                {
                    IdentityResult result = await _userManager.UpdateSecurityStampAsync(user);
                    isUpdated = result.Succeeded;
                }
            }

            return new BaseReponse()
            {
                Message = $"{isUpdated}",
                StatusCode = (int)(isUpdated ? HttpStatusCode.OK : HttpStatusCode.BadRequest),
            };
        }

        public async Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime accessTokenLifeTime, int addMinuteToLifeTime)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = accessTokenLifeTime.AddMinutes(addMinuteToLifeTime);
            await _userManager.UpdateAsync(user);
        }

        public async Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user is not null && user.RefreshTokenExpirationDate > DateTime.UtcNow)
            {
                TokenResponseDto tokenResponse = await _tokenHandler.CreateAccessTokenAsync(user, 10);
                await UpdateRefreshToken(user, tokenResponse.RefreshToken, tokenResponse.Expiration, 5);
                return tokenResponse;
            }
            throw new TokenFailedException();
        }

    }
}
