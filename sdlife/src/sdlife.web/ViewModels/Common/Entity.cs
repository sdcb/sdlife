using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.ViewModels.Common
{
    public class Entity<T> where T: class
    {
        public int Id { get; set; }
    }
}
