using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sdlife.web.Services.Implements
{
    public class TestCurrentUser : ICurrentUser
    {
        private readonly HttpContext _httpContext;

        public TestCurrentUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
        }

        public bool IsSignedIn
        {
            get
            {
                return true;
            }
        }

        public int UserId
        {
            get
            {
                return 1;
            }
        }

        public string UserName
        {
            get
            {
                return "sdflysha@qq.com";
            }
        }

        public string GetClaim(string claimType)
        {
            throw new NotImplementedException();
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}
