using Chatting_System.Dtos.Account;
using Chatting_System.Interfaces;
using Chatting_System.Mappers;
using Chatting_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chatting_System.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if(_userManager.Users.Any(u => u.UserName.Equals(registerDto.UserName)))
                {
                    return BadRequest("Username is used");
                }
                if (_userManager.Users.Any(u => u.Email.Equals(registerDto.Email)))
                {
                    return BadRequest("Email is used");
                }
                AppUser appUser = registerDto.FromRegisterDtoToAppUser();
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    string token = _tokenService.CreateToken(appUser);
                    UserDto userDto = appUser.FromAppUserToUserDto(token);
                    return Ok(userDto);
                }
                return StatusCode(500, createdUser.Errors.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }
            AppUser? appUser = _userManager.Users.FirstOrDefault(u => u.UserName == loginDto.UserName);
            if (appUser == null)
            {
                return Unauthorized("Invalid Username");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid Password");
            }
            string token = _tokenService.CreateToken(appUser);
            UserDto userDto = appUser.FromAppUserToUserDto(token);
            return Ok(userDto);
        }
    }
}
