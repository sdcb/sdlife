using sdlife.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Dtos.Diary
{
    public class DiaryQuery : PagedListQuery
    {
        public int UserId { get; set; }
    }
}
