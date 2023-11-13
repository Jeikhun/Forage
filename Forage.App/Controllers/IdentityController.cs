using Forage.Service.Dtos.Accounts;
using Forage.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forage.App.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        readonly IIdentityUserService _identityService;

        public IdentityController(IIdentityUserService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        [ActionName("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _identityService.GetAllUsers();
            return Ok(response);
        }

        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await _identityService.Register(dto, "Intern");
            return Ok(response);
        }

        [HttpPut]
        [ActionName("Update")]
        public async Task<IActionResult> Update(UpdateDto dto)
        {
            var response = await _identityService.UpdateUser(dto);
            return Ok(response);
        }

        [HttpGet]
        [ActionName("GetCurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            return Ok(await _identityService.GetCurrentUser());
        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _identityService.Login(dto, 30);
            return Ok(response);
        }
        //[HttpPost]
        //public async Task<IActionResult> AddRole()
        //{
        //    IdentityRole role = new IdentityRole
        //    {
        //        Name = "Intern"
        //    };
        //    IdentityRole role1 = new IdentityRole
        //    {
        //        Name = "Company"
        //    };
        //    IdentityRole role2 = new IdentityRole
        //    {
        //        Name = "Admin"
        //    };
        //    IdentityRole role3 = new IdentityRole
        //    {
        //        Name = "SuperAdmin"
        //    };
        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);

        //    return Ok("Good");
        //}

        //[HttpPost("google-login")]
        //public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto googleLoginDto)
        //{
        //    var response = await _identityService.GoogleAuthorize(googleLoginDto.Token);
        //    return Ok(response);
        //}
    }
}
