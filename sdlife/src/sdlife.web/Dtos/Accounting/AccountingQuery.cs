using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Dtos
{
    public class AccountingQuery
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int? UserId { get; set; }
    }
}
