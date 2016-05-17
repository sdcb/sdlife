using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace sdlife.web.Services.Implements
{
    public class TestCurrentUser : ICurrentUser
    {
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
                return 7;
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
