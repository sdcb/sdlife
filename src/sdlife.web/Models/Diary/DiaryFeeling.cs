using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class DiaryFeeling
    {
        [Key]
        [ForeignKey(nameof(Diary))]
        public int DiaryId { get; set; }

        [Key]
        [ForeignKey(nameof(Feeling))]
        public int FeelingId { get; set; }

        public DiaryHeader Diary { get; set; }

        public Feeling Feeling { get; set; }
    }
}
