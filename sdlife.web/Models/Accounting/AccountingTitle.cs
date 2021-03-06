﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class AccountingTitle
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string ShortCut { get; set; }

        public bool IsIncome { get; set; }

        public HashSet<Accounting> Accountings { get; set; } = new HashSet<Accounting>();
    }
}
