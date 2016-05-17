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
        public HashSet<Accounting> Accountings { get; set; } = new HashSet<Accounting>();

        public HashSet<AccountingTitle> AccountingTitles { get; set; } = new HashSet<AccountingTitle>();
    }

    public class Role : IdentityRole<int>
    {
    }
}
