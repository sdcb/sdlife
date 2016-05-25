using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sdlife.web.Dtos;
using Microsoft.AspNetCore.Identity;
using sdlife.web.Models;
using sdlife.web.Managers;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace sdlife.web.Controllers
{
    public class AccountController : SdlifeBaseController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly SdlifeUserManager _userManager;

        public AccountController(
            SdlifeUserManager userManager, 
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

        public int DoWork()
        {
            return 3;
        }
    }
}
