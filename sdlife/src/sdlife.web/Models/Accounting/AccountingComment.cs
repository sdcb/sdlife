using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class AccountingComment
    {
        [Key]
        public int AccountingId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Comment { get; set; }

        public Accounting Accounting { get; set; }
    }
}
