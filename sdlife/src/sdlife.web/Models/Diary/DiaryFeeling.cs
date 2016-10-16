using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class DiaryFeeling
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public int Name { get; set; }
    }
}
