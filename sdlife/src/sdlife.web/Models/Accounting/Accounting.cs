using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class Accounting
    {
        public int Id { get; set; }

        public int TitleId { get; set; }

        public AccountingTitle Title { get; set; }

        public DateTime EventTime { get; set; } = DateTime.Now;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        public int CreateUserId { get; set; }

        public User CreateUser { get; set; }

        public AccountingComment Comment { get; set; }
    }
}
