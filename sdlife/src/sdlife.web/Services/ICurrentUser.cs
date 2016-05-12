using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Services
{
    public interface ICurrentUser
    {
        int UserId { get; }

        string UserName { get; }

        bool IsSignedIn { get; }

        bool IsInRole(string role);

        string GetClaim(string claimType);
    }
}
