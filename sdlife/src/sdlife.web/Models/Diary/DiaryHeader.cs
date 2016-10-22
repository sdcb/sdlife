using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class DiaryHeader
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int WeatherId { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;

        public Weather Weather { get; set; }

        public User User { get; set; }

        public DiaryContent Content { get; set; }

        public ICollection<DiaryFeeling> Feelings { get; set; } = new HashSet<DiaryFeeling>();
    }
}
