using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sdlife.web.Dtos;
using Microsoft.AspNetCore.Identity;
using sdlife.web.Models;
using sdlife.web.Managers;
using Microsoft.AspNetCore.Antiforgery;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace sdlife.web.Controllers
{
    public class AccountController : SdlifeBaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly SdlifeUserManager _userManager;
        private readonly IAntiforgery _antiforgery;
        private readonly IConfigurationRoot _config;

        public AccountController(
            SdlifeUserManager userManager,
            SignInManager<User> signInManager,
            IAntiforgery antiforgery, 
            IConfigurationRoot config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _antiforgery = antiforgery;
            _config = config;
        }

        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByNameOrEmailAsync(loginDto.UserName);
            if (user == null)
            {
                return NotFound();
            }

            var passwordOk = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordOk)
            {
                return NotFound();
            }

            await _signInManager.SignInAsync(user, loginDto.RememberMe);
            return Ok();
        }

        public async Task<IActionResult> CreateToken([FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByNameOrEmailAsync(loginDto.UserName);
            if (user == null)
            {
                return NotFound();
            }

            var passwordOk = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordOk)
            {
                return NotFound();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), 
                new Claim(JwtRegisteredClaimNames.Jti, Convert.ToBase64String(Guid.NewGuid().ToByteArray())), 
                new Claim(JwtRegisteredClaimNames.Email, user.Email), 
            }.Concat(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow, 
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: cred);

            return Json(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token), 
                Expiration = token.ValidTo
            });
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user,
                changePasswordDto.CurrentPassword,
                changePasswordDto.NewPassword);
            return FromIdentityResult(result);
        }

        public async Task<IActionResult> Register([FromBody]CreateUserDto createUserDto)
        {
            var user = (User)createUserDto;
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            return FromIdentityResult(result);
        }

        public void CsrfCookie()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }

        public string RefreshCsrf()
        {
            return _antiforgery.GetTokens(HttpContext).RequestToken;
        }

        private IActionResult FromIdentityResult(IdentityResult result)
        {
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
