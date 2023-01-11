using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.Api.Admin.Dtos.AccountDtos;
using Store.Api.Services;
using Store.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Store.Api.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin")]
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _confg;
        private readonly IJwtService _jwtService;

        public AccountsController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration confg,IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _confg = confg;
            _jwtService = jwtService;
        }

        //[HttpGet("roles")]
        //public async Task<IActionResult> Create()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));

        //    return Ok();
        //}


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null || user.IsMember)
                return BadRequest();

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return BadRequest();
            var roles = await _userManager.GetRolesAsync(user);
         

            return Ok(new {token = _jwtService.GenerateToken(user,roles,_confg)});
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                UserName = "SuperAdmin",
                FullName = "Hikmet Abbasov",
            };

            await _userManager.CreateAsync(admin, "Admin123");
            await _userManager.AddToRoleAsync(admin, "SuperAdmin");

            return Ok();
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {

            return Ok(new { username = User.Identity.Name });
        } 


    }
}
