using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Dtos
{
    public class AccountingPagedListQuery : PagedListQuery
    {
        public int? UserId { get; set; }

        public string Title { get; set; }

        public List<string> Titles { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public bool? IsIncome { get; set; }

        public decimal? MinAmount { get; set; }

        public decimal? MaxAmount { get; set; }
    }
}
