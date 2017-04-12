using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sdlife.web.Services.Implements
{
    public class CurrentUser : ICurrentUser
    {
        private readonly HttpContext _httpContext;
        private readonly UserManager<User> _userManager;

        public CurrentUser(IHttpContextAccessor httpContext, 
            UserManager<User> userManager)
        {
            _httpContext = httpContext.HttpContext;
            _userManager = userManager;
        }

        public bool IsSignedIn
            => _httpContext.User.Identity.IsAuthenticated;

        public int UserId
            => int.Parse(_userManager.GetUserId(_httpContext.User));

        public string UserName
            => _userManager.GetUserName(_httpContext.User);

        public bool RememberMe
            => bool.Parse(GetClaim("RememberMe"));

        public string GetClaim(string claimType)
        {
            return _httpContext.User.FindFirstValue(claimType);
        }

        public bool IsInRole(string role)
        {
            return _httpContext.User.IsInRole(role);
        }
    }
}
