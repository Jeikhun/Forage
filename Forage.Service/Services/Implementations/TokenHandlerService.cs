using Forage.Core.Entities;
using Forage.Service.Dtos.Tokens;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Forage.Service.Services.Implementations
{
    public class TokenHandler:ITokenHandler
    {
        readonly UserManager<AppUser> _userManager;
        readonly IConfiguration _configuration;

        public TokenHandler(UserManager<AppUser> userManager, IConfiguration configuratixon)
        {
            _userManager = userManager;
            _configuration = configuratixon;
        }

        public async Task<TokenResponseDto> CreateAccessTokenAsync(AppUser user, int minute)
        {
            TokenResponseDto tokenResponseDto = new();
            tokenResponseDto.Expiration = DateTime.UtcNow.AddHours(4).AddMinutes(minute);
            List<Claim> myClaims = new() {
               new(ClaimTypes.Name,user.UserName),
               new(ClaimTypes.Email,user.Email),
               new(ClaimTypes.NameIdentifier,user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                myClaims.Add(new(ClaimTypes.Role, role));
            }
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtTokenSettings:SignInKey").Value));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _configuration["JwtTokenSettings:ValidIssuer"],
                audience: _configuration["JwtTokenSettings:ValidAudience"],
                claims: myClaims,
                notBefore: DateTime.UtcNow.AddMinutes(120),
                expires: tokenResponseDto.Expiration,
                signingCredentials: signingCredentials
                );


            JwtSecurityTokenHandler tokenHandler = new();
            tokenResponseDto.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            tokenResponseDto.RefreshToken = CreateRefreshToken();
            return tokenResponseDto;
        }

        public string CreateRefreshToken()
        {
            byte[] bytes = new byte[64];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
