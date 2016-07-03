using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.Models
{
    public class PagedList<T>
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}
