using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class DiaryContent
    {
        [Key]
        [ForeignKey(nameof(Diary))]
        public int DiaryId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Content { get; set; }

        public DiaryHeader Diary { get; set; }
    }
}
