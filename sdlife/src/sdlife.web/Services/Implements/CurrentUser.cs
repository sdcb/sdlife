﻿using Microsoft.AspNet.Http;
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

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
        }

        public bool IsSignedIn
        {
            get
            {
                return _httpContext.User.IsSignedIn();
            }
        }

        public int UserId
        {
            get
            {
                return int.Parse(_httpContext.User.GetUserId());
            }
        }

        public string UserName
        {
            get
            {
                return _httpContext.User.GetUserName();
            }
        }

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
