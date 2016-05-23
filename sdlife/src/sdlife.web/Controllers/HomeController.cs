using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using sdlife.web.Models;

namespace sdlife.web.Controllers
{
    public class HomeController : Controller
    {
        public readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<User> CreateTestUser()
        {
            var user = new User
            {
                UserName = "test", 
                Email = "test@qq.com", 
            };
            var findedUser = await _userManager.FindByNameAsync(user.UserName);
            if (findedUser != null)
            {
                return findedUser;
            }
            else
            {
                await _userManager.CreateAsync(user);
                return await _userManager.FindByNameAsync(user.UserName);
            }
        }
    }
}
