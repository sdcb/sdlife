using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace sdlife.web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser<int>
    {
        public HashSet<Accounting> Accountings { get; set; } 
            = new HashSet<Accounting>();

        public HashSet<AccountingUserAuthorization> AccountingUserAuthorizationFrom { get; set; }
            = new HashSet<AccountingUserAuthorization>();

        public HashSet<AccountingUserAuthorization> AccountingUserAuthorizationTarget { get; set; }
            = new HashSet<AccountingUserAuthorization>();
    }

    public class Role : IdentityRole<int>
    {
    }
}
