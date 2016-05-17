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
            return RedirectToAction("Index", "Accounting");
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<object> CreateTestAccount()
        {
            var userName = Guid.NewGuid().ToString();
            var ok = await _userManager.CreateAsync(new User
            {
                UserName = userName,
                Email = userName + "@qq.com", 
            }, "Passw0rd!");
            if (ok.Succeeded)
            {
                return await _userManager.FindByNameAsync(userName);
            }
            else
            {
                return ok.Errors;
            }
        }
    }
}
