using Forage.Service.Dtos.Auths;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forage.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var response = await _authService.ResetPassword(resetPasswordDto.Email);
            return Ok(response);
        }

        [HttpPost]
        [ActionName("VerifyPasswordResetToken")]
        public async Task<IActionResult> VerifyPasswordResetToken(ResetResponseDto dto)
        {
            var response = await _authService.VerifyPasswordResetToken(dto);
            return Ok(response);
        }

        [HttpPost]
        [ActionName("UpdatePasswordAsync")]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordDto dto)
        {
            var response = await _authService.UpdatePasswordAsync(dto);
            return Ok(response);
        }

        [HttpPost]
        [ActionName("RefreshTokenLoginAsync")]
        public async Task<IActionResult> RefreshTokenLoginAsync(string refreshToken)
        {
            var response = await _authService.RefreshTokenLoginAsync(refreshToken);
            return Ok(response);
        }
    }
}
