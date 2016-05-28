using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class AccountingUserAuthorization
    {
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [ForeignKey("AuthorizedUser")]
        public int AuthorizedUserId { get; set; }

        public AccountingAuthorizeLevel Level { get; set; }

        public User User { get; set; }

        public User AuthorizedUser { get; set; }
    }

    [Flags]
    public enum AccountingAuthorizeLevel
    {
        None = 0, 
        QuerySpendings = 1, 
        QueryIncomes = 2, 
        QueryAll = 3, 
    }
}
