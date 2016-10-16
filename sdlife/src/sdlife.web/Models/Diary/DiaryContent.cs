using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models.Diary
{
    public class DiaryContent
    {
        public int DiaryId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Content { get; set; }
    }
}
