using sdlife.web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sdlife.web.unittest.Mocked
{
    public class TestCurrentUser : ICurrentUser
    {
        public bool IsSignedIn
            => true;

        public int UserId
            => 7;

        public string UserName
            => "example@example.com";

        public bool RememberMe
            => false;

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
