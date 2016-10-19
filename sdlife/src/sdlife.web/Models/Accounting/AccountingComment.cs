using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class AccountingComment
    {
        [Key]
        [ForeignKey(nameof(Accounting))]
        public int AccountingId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Comment { get; set; }
        
        public Accounting Accounting { get; set; }
    }
}
